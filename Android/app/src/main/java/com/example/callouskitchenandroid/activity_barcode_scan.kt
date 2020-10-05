package com.example.callouskitchenandroid

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.Manifest
import android.content.pm.PackageManager
import android.util.Log
import android.widget.Toast
import androidx.core.app.ActivityCompat
import android.content.Intent
import androidx.core.content.ContextCompat
import java.util.concurrent.Executors
import androidx.camera.core.*
import androidx.camera.lifecycle.ProcessCameraProvider
import com.android.volley.Response
import com.google.mlkit.vision.barcode.Barcode
import com.google.mlkit.vision.barcode.BarcodeScannerOptions
import com.google.mlkit.vision.barcode.BarcodeScanning
import com.google.mlkit.vision.common.InputImage
import kotlinx.android.synthetic.main.activity_barcode_scan.*
import org.json.JSONObject
import java.io.File
import java.time.LocalDate
import java.time.format.DateTimeFormatter
import java.util.concurrent.ExecutorService
typealias ResultListener = (result: String?) -> Unit

class activity_barcode_scan : AppCompatActivity() {

    private lateinit var cameraExecutor: ExecutorService

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_barcode_scan)

        // Request camera permissions
        if (allPermissionsGranted()) {
            startCamera()
        } else {
            ActivityCompat.requestPermissions(
                this, REQUIRED_PERMISSIONS, REQUEST_CODE_PERMISSIONS)
        }

        cameraExecutor = Executors.newSingleThreadExecutor()
    }

    private fun allPermissionsGranted() = REQUIRED_PERMISSIONS.all {
        ContextCompat.checkSelfPermission(
            baseContext, it) == PackageManager.PERMISSION_GRANTED
    }

    override fun onDestroy() {
        super.onDestroy()
        cameraExecutor.shutdown()
    }

    companion object {
        private const val TAG = "CameraXBasic"
        private const val REQUEST_CODE_PERMISSIONS = 10
        private val REQUIRED_PERMISSIONS = arrayOf(Manifest.permission.CAMERA)
    }

    override fun onRequestPermissionsResult(
        requestCode: Int, permissions: Array<String>, grantResults:
        IntArray) {
        if (requestCode == REQUEST_CODE_PERMISSIONS) {
            if (allPermissionsGranted()) {
                startCamera()
            } else {
                Toast.makeText(this,
                    "Permissions not granted by the user.",
                    Toast.LENGTH_SHORT).show()
                finish()
            }
        }
    }
    private fun startCamera() {
        val cameraProviderFuture = ProcessCameraProvider.getInstance(this)

        cameraProviderFuture.addListener(Runnable {
            // Used to bind the lifecycle of cameras to the lifecycle owner
            val cameraProvider: ProcessCameraProvider = cameraProviderFuture.get()

            // Preview
            val preview = Preview.Builder()
                .build()
                .also {
                    it.setSurfaceProvider(viewFinder.createSurfaceProvider())
                }

            //This is my code where I get stuff done
            val imageAnalyzer = ImageAnalysis.Builder()
                .build()
                .also {
                    it.setAnalyzer(cameraExecutor, ImageAnalyzer{ result -> //Result is the barcode
                        if (!result.isNullOrEmpty()){ //Only proceed if barcode was found
                            ServiceHandler.callOpenFoodFacts(
                                result,this,
                                Response.Listener { response ->
                                    val json = JSONObject(response.toString())
                                    val product = json.optJSONObject("product")
                                    //If product is null these are all null
                                    val foodName = product?.optString("product_name")
                                    //TODO: Find a better way to use quantity
                                    val quantity = ExtrapolateQuantity(product?.optString("quantity"))
                                    val expirationDate = StringToDate(product?.optString("expiration_date"))
                                    //To be implemented
                                    val pictureUrl = product?.optString("image_thumb_url")
                                    val categories = StringToArray(product?.optString("categories"))
                                    //TODO: Get these 3 values into an add food page

                                    // Send values to "AddFoodActivity"
                                    Log.i("superfood", "foodname:$foodName quantity:$quantity expiration:$expirationDate")

                                    if (foodName == null && quantity == null && expirationDate == null)
                                    {
                                        Toast.makeText(applicationContext,"No data found", Toast.LENGTH_LONG).show()
                                    }

                                    val intent = Intent(this, AddFoodActivity::class.java)
                                    intent.putExtra("FOODNAME", foodName)
                                    intent.putExtra("QUANTITY", quantity)
                                    intent.putExtra("EXPIRY", expirationDate)

                                    startActivity(intent)
                                })
                        }
                    })
                }

            // Select back camera as a default
            val cameraSelector = CameraSelector.DEFAULT_BACK_CAMERA

            try {
                // Unbind use cases before rebinding
                cameraProvider.unbindAll()

                // Bind use cases to camera
                cameraProvider.bindToLifecycle(
                    this, cameraSelector, preview, imageAnalyzer)

            } catch(exc: Exception) {
                Log.e(TAG, "Use case binding failed", exc)
            }

        }, ContextCompat.getMainExecutor(this))
    }

    private fun ExtrapolateQuantity(quantity : String?) : Double?{
        if (quantity.isNullOrEmpty())
            return null
        else
            return quantity.filter { it.isDigit() }.toDoubleOrNull()
    }

    private fun StringToDate(date : String?) : LocalDate?{
        if (date.isNullOrEmpty())
            return null
        else
            return LocalDate.parse(
                date,
                DateTimeFormatter.ofPattern("yyyy/MM/dd")
            )
    }

    //Remove spaces and split
    private fun StringToArray(commaDelimitedArray : String?) : Array<String>?{
        if (commaDelimitedArray.isNullOrEmpty())
            return null
        else
            return commaDelimitedArray.replace("\\s".toRegex(), "").split(",").toTypedArray()
    }

    //This gets run whenever there's new camera info
    private class ImageAnalyzer(private val listener : ResultListener) : ImageAnalysis.Analyzer {

        @androidx.camera.core.ExperimentalGetImage
        override fun analyze(imageProxy: ImageProxy) {
            //Conversions
            val mediaImage = imageProxy.image
            if (mediaImage != null) {
                val image = InputImage.fromMediaImage(mediaImage, imageProxy.imageInfo.rotationDegrees)
                scanBarcodes(image, imageProxy);
            }
            else
                imageProxy.close()
        }
        fun scanBarcodes(image: InputImage, imageProxy : ImageProxy) {
            // [START set_detector_options]
            //I though upc and product were the same thing but they're seperate here so accept either
            val options = BarcodeScannerOptions.Builder()
                .setBarcodeFormats(
                    Barcode.TYPE_PRODUCT,
                    Barcode.FORMAT_UPC_A)
                .build()
            // [END set_detector_options]

            // [START get_detector]
            //val scanner = BarcodeScanning.getClient()
            // Or, to specify the formats to recognize:
            val scanner = BarcodeScanning.getClient(options)
            // [END get_detector]

            // [START run_detector]
            val result = scanner.process(image)
                .addOnSuccessListener { barcodes ->
                    // Task completed successfully
                    // [START_EXCLUDE]
                    // [START get_barcodes]
                    for (barcode in barcodes) {
                        val rawValue = barcode.rawValue

                        val valueType = barcode.valueType
                        // See API reference for complete list of supported types
                        if (valueType == Barcode.TYPE_PRODUCT || valueType == Barcode.FORMAT_UPC_A) {
                            //basically this returns the result in a listener
                            listener(rawValue)
                        }
                    }
                    // [END get_barcodes]
                    // [END_EXCLUDE]
                    //Important, only close the imageProxy once everything's been processed
                    imageProxy.close()
                }
                .addOnFailureListener {
                    // Task failed with an exception
                    listener(null)
                    //Important, only close the imageProxy once everything's been processed
                    imageProxy.close()
                }
            // [END run_detector]
        }
    }

}
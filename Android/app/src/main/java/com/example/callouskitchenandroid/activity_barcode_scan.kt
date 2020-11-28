package com.example.callouskitchenandroid

import android.Manifest
import android.content.Intent
import android.content.pm.PackageManager
import android.os.Bundle
import android.util.Log
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.camera.core.CameraSelector
import androidx.camera.core.ImageAnalysis
import androidx.camera.core.ImageProxy
import androidx.camera.core.Preview
import androidx.camera.lifecycle.ProcessCameraProvider
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import com.android.volley.Response
import com.google.mlkit.vision.barcode.Barcode
import com.google.mlkit.vision.barcode.BarcodeScannerOptions
import com.google.mlkit.vision.barcode.BarcodeScanning
import com.google.mlkit.vision.common.InputImage
import kotlinx.android.synthetic.main.activity_barcode_scan.*
import org.json.JSONObject
import java.time.LocalDate
import java.time.format.DateTimeFormatter
import java.util.concurrent.ExecutorService
import java.util.concurrent.Executors

typealias ResultListener = (result: String) -> Unit

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
                this, REQUIRED_PERMISSIONS, REQUEST_CODE_PERMISSIONS
            )
        }

        cameraExecutor = Executors.newSingleThreadExecutor()
    }

    private fun allPermissionsGranted() = REQUIRED_PERMISSIONS.all {
        ContextCompat.checkSelfPermission(
            baseContext, it
        ) == PackageManager.PERMISSION_GRANTED
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
        IntArray
    ) {
        if (requestCode == REQUEST_CODE_PERMISSIONS) {
            if (allPermissionsGranted()) {
                startCamera()
            } else {
                Toast.makeText(
                    this,
                    "Permissions not granted by the user.",
                    Toast.LENGTH_SHORT
                ).show()
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
                    it.setAnalyzer(cameraExecutor, ImageAnalyzer { result -> //Result is the barcode
                        it.clearAnalyzer()
                        ServiceHandler.callOpenFoodFacts(
                            result, this,
                            Response.Listener { response ->
                                val json = JSONObject(response.toString())
                                val product = json.optJSONObject("product")
                                //If product is null these are all null
                                val foodName = product?.optString("product_name")
                                val sourceQuantity = product?.optString("quantity")
                                var quantity = ExtrapolateQuantity(sourceQuantity)
                                var quantityClassifer = ExtrapolateQuantityClassifier(
                                    sourceQuantity
                                )
                                val expirationDate =
                                    StringToDate(product?.optString("expiration_date"))
                                //To be implemented
                                val ingredientsAnalysis =
                                    product?.optJSONArray("ingredients_analysis_tags")
                                //1 means true, 0 means false, -1 means unknown
                                var vegan: Int? = -1
                                var vegetarian: Int? = -1
                                for (i in 0 until (ingredientsAnalysis?.length() ?: 0)) {
                                    val item = RemovePrefix(ingredientsAnalysis!![i].toString())
                                    when (item) {
                                        "vegan" -> vegan = 1
                                        "non-vegan" -> vegan = 0
                                        "vegan-status-unknown" -> vegan = -1
                                        "vegetarian" -> vegetarian = 1
                                        "non-vegetarian" -> vegetarian = 0
                                        "vegetarian-status-unknown" -> vegetarian = -1
                                    }
                                }
                                var traces = StringToArray(product?.optString("traces"))
                                var ingredientsTags = product?.optJSONArray("ingredients_tags")
                                var ingredients =
                                    Array<String>(ingredientsTags?.length() ?: 0) { "n = $it" }
                                for (i in 0 until (ingredientsTags?.length() ?: 0)) {
                                    ingredients[i] =
                                        FormatString(ingredientsTags!![i].toString())
                                }

                                val intent = Intent(this, AddFoodActivity::class.java)
                                intent.putExtra("FOODNAME", foodName)
                                intent.putExtra("QUANTITY", quantity)
                                intent.putExtra("QUANTITYCLASSIFIER", quantityClassifer)
                                val formatter = DateTimeFormatter.ofPattern("MM/dd/yyyy")
                                intent.putExtra("EXPIRY", expirationDate?.format(formatter))
                                intent.putExtra("VEGAN", vegan)
                                intent.putExtra("VEGETARIAN", vegetarian)
                                intent.putExtra("INGREDIENTS", ingredients)
                                intent.putExtra("TRACES", traces)


                                startActivity(intent)
                            })
                    })
                }

            // Select back camera as a default
            val cameraSelector = CameraSelector.DEFAULT_BACK_CAMERA

            try {
                // Unbind use cases before rebinding
                cameraProvider.unbindAll()

                // Bind use cases to camera
                cameraProvider.bindToLifecycle(
                    this, cameraSelector, preview, imageAnalyzer
                )

            } catch (exc: Exception) {
                Log.e(TAG, "Use case binding failed", exc)
            }

        }, ContextCompat.getMainExecutor(this))
    }

    private fun ExtrapolateQuantity(quantity: String?) : Double{
        if (quantity.isNullOrEmpty())
            return 0.0
        else
            return quantity.filter { it.isDigit() }.toDoubleOrNull() ?: 0.0
    }

    private fun ExtrapolateQuantityClassifier(quantity: String?) : String{
        if (quantity.isNullOrEmpty())
            return "item"
        else
            {
                //Honestly, the quantity has always been in g or ml so this is probably overkill
                var orderedClassifiers = listOf<String>(
                    "kg",
                    "mg",
                    "mL",
                    "oz",
                    "gallon",
                    "lb",
                    "g",
                    "L"
                )
                //One custom search and return because fl oz is hard
                if (quantity!!.toLowerCase().contains("fl") && quantity!!.toLowerCase().contains("oz")){
                    return "fl. oz."
                }
                orderedClassifiers.forEach{
                    if (quantity!!.toLowerCase().contains(it.toLowerCase())){
                        return it
                    }
                }
                return "item"
            }
    }

    private fun StringToDate(date: String?) : LocalDate?{
        if (date.isNullOrEmpty())
            return null
        else{
            //Try the different possible date formats and hope that there aren't any more possibilities
            try{
                return LocalDate.parse(
                    date,
                    DateTimeFormatter.ofPattern("yyyy/MM/dd")
                )
            }
            catch (exc: Exception) {
                println("yyyy/MM/dd didn't work")
            }
            try{
                return LocalDate.parse(
                    date,
                    DateTimeFormatter.ofPattern("dd/MM/yyyy")
                )
            }
            catch (exc: Exception) {
                println("dd/MM/yyyy didn't work")
            }
            try{
                return LocalDate.parse(
                    date,
                    DateTimeFormatter.ofPattern("MM/d/yyyy")
                )
            }
            catch (exc: Exception) {
                println("MM/dd/yyyy didn't work")
            }
        }
        return null
    }

    private fun FormatString(theString: String) : String{
        var formattedString = RemovePrefix(theString)
        formattedString = formattedString.trim()
        formattedString = formattedString.replace('-', ' ')
        formattedString = formattedString.replace('_', ' ')
        return formattedString
    }

    private fun RemovePrefix(textWithPrefix: String) : String{
        val colonLocation = textWithPrefix.indexOf(':')
        if (colonLocation != -1)
            return textWithPrefix.substring(colonLocation + 1, textWithPrefix.length)
        return textWithPrefix
    }

    //Remove spaces and split
    private fun StringToArray(commaDelimitedArray: String?) : Array<String>?{
        if (commaDelimitedArray.isNullOrEmpty())
            return null
        else
            return commaDelimitedArray.split(",").map { FormatString(it) }.toTypedArray()
    }

    //This gets run whenever there's new camera info
    private class ImageAnalyzer(private val listener: ResultListener) : ImageAnalysis.Analyzer {

        @androidx.camera.core.ExperimentalGetImage
        override fun analyze(imageProxy: ImageProxy) {
                //Conversions
                val mediaImage = imageProxy.image
                if (mediaImage != null) {
                    val image = InputImage.fromMediaImage(
                        mediaImage,
                        imageProxy.imageInfo.rotationDegrees
                    )
                    scanBarcodes(image, imageProxy);
                }
                else
                    imageProxy.close()
        }
        fun scanBarcodes(image: InputImage, imageProxy: ImageProxy) {
            // [START set_detector_options]
            //I though upc and product were the same thing but they're seperate here so accept either
            val options = BarcodeScannerOptions.Builder()
                .setBarcodeFormats(
                    Barcode.TYPE_PRODUCT,
                    Barcode.FORMAT_UPC_A
                )
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
                            if (!rawValue.isNullOrEmpty()) { //Only proceed if barcode was found
                                //basically this returns the result in a listener
                                listener(rawValue)
                            }
                        }
                    }
                    // [END get_barcodes]
                    // [END_EXCLUDE]
                    //Important, only close the imageProxy once everything's been processed
                    imageProxy.close()
                }
                .addOnFailureListener {
                    //Important, only close the imageProxy once everything's been processed
                    imageProxy.close()
                }
            // [END run_detector]
        }
    }

}
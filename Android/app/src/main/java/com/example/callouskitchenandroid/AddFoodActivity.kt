package com.example.callouskitchenandroid

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import com.android.volley.Response
import kotlinx.android.synthetic.main.activity_add_food.view.*
import org.json.JSONObject
import org.json.JSONObject.NULL
import android.app.DatePickerDialog
import java.util.*
import android.widget.DatePicker
import java.text.SimpleDateFormat
import java.time.LocalDate
import java.time.format.DateTimeFormatter
import java.time.format.DateTimeParseException
import android.util.Log

class AddFoodActivity : AppCompatActivity() {

    companion object {
        private const val ADD_FOOD_TAG = "AddFoodActivity"
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_add_food)

        val txtFoodName = findViewById<EditText>(R.id.editAddFoodName)
        val txtFoodQuantity = findViewById<EditText>(R.id.editAddFoodQuantity)
        val txtFoodExpiry = findViewById<EditText>(R.id.editTextAddFoodExpiry)
        val btnAddFood = findViewById<Button>(R.id.btnAddFoodItem)
        val btnCancel = findViewById<Button>(R.id.btnCancelAddFood)
        val btnScanBarcode = findViewById<Button>(R.id.btnScanBarcode)
        var kitchenId : Int = 0

        var cal = Calendar.getInstance()

        // Get the food data from the barcode scanner
        if (intent != null)
        {
            var foodName : String?
            var quantity : String?
            var expiryDate : String?

            kitchenId = intent.getIntExtra("kitchenId", 0)

            // Get the food name
            try
            {
                foodName = intent.getStringExtra("FOODNAME")
                txtFoodName.setText(foodName)
            }
            catch (exc: Exception)
            {
                // Log.e(activity_barcode_scan.TAG, "Use case binding failed", exc)
                Log.i(ADD_FOOD_TAG, "'FOODNAME' data could not be found", exc)
            }

            // Get the quantity
            try
            {
                quantity = intent.getStringExtra("QUANTITY")
                txtFoodQuantity.setText(quantity)
            }
            catch (exc: Exception)
            {
                // Log.e(activity_barcode_scan.TAG, "Use case binding failed", exc)
                Log.i(ADD_FOOD_TAG, "'QUANTITY' data could not be found", exc)
            }

            // Get the expiry date
            try
            {
                expiryDate = intent.getStringExtra("EXPIRY")
                txtFoodExpiry.setText(expiryDate)
            }
            catch (exc: Exception)
            {
                // Log.e(activity_barcode_scan.TAG, "Use case binding failed", exc)
                Log.i(ADD_FOOD_TAG, "'EXPIRY' data could not be found", exc)
            }
        }

        val dateSetListener = object : DatePickerDialog.OnDateSetListener {
            override fun onDateSet(view: DatePicker, year: Int, monthOfYear: Int,
                                   dayOfMonth: Int) {
                cal.set(Calendar.YEAR, year)
                cal.set(Calendar.MONTH, monthOfYear)
                cal.set(Calendar.DAY_OF_MONTH, dayOfMonth)
                val myFormat = "MM/dd/yyyy" // mention the format you need
                val sdf = SimpleDateFormat(myFormat, Locale.US)
                txtFoodExpiry.setText(sdf.format(cal.getTime()))
            }
        }

        // Go to the barcode scanner
        btnScanBarcode.setOnClickListener{
            val intent = Intent(this@AddFoodActivity, activity_barcode_scan::class.java)
            intent.putExtra("kitchenId",kitchenId)
            startActivity(intent)
        }

        btnAddFood.setOnClickListener{
            val foodName = txtFoodName.text.toString()

            if (foodName.isNullOrEmpty())
            {
                Toast.makeText(applicationContext,"Please enter the food name", Toast.LENGTH_LONG).show()
            }
            else
            {
                val quantityString = txtFoodQuantity.text.toString()
                var quantity: Double = 1.0
                if (!quantityString.isNullOrEmpty())
                    quantity = quantityString.toDouble()
                var expiryDate : LocalDate
                try{
                    expiryDate = LocalDate.parse(txtFoodExpiry.text.toString(), DateTimeFormatter.ofPattern("MM/dd/yyyy"))
                }
                catch(e : DateTimeParseException){
                    Toast.makeText(applicationContext,"Please enter a valid date", Toast.LENGTH_LONG).show()
                    return@setOnClickListener
                }
               ServiceHandler.callAccountService(
                    "AddFood",hashMapOf(
                       "kitchenId" to intent.getIntExtra("kitchenId", 0)
                       ,"name" to foodName, "quantity" to quantity
                       , "expiryDate" to ServiceHandler.serializeDate(expiryDate)),this,
                    Response.Listener { response ->
                        val json = JSONObject(response.toString())
                        val kitchenId = json.getBoolean("AddFoodResult")
                        // go to food inventory
                        println(kitchenId)
                        // return to the food list
                        val intent = Intent(this@AddFoodActivity, InventoryActivity::class.java)
                        startActivity(intent)

                    })
            }
        }

        btnCancel.setOnClickListener{
            val intent = Intent(this@AddFoodActivity, InventoryActivity::class.java)
            startActivity(intent)
        }

        txtFoodExpiry.setOnFocusChangeListener { v, hasFocus ->
            DatePickerDialog(this@AddFoodActivity,
                dateSetListener,
                // set DatePickerDialog to point to today's date when it loads up
                cal.get(Calendar.YEAR),
                cal.get(Calendar.MONTH),
                cal.get(Calendar.DAY_OF_MONTH)).show()
        }

    }
}

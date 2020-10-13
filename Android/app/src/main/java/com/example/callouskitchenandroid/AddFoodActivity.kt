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
import org.json.JSONArray

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
        var foodName : String?
        var quantity : String?
        var expiryDate : String?
        var vegan : Int? = -1
        var vegetarian : Int? = -1
        var ingredients = arrayOf<String>()
        var traces = arrayOf<String>()

        // Get the food data from the barcode scanner
        if (intent != null)
        {

            kitchenId = intent.getIntExtra("kitchenId", 0)

            try
            {
                foodName = intent.getStringExtra("FOODNAME")
                txtFoodName.setText(foodName)
            }
            catch (exc: Exception)
            {}

            try
            {
                quantity = intent.getStringExtra("QUANTITY")
                txtFoodQuantity.setText(quantity)
            }
            catch (exc: Exception)
            {}

            try
            {
                expiryDate = intent.getStringExtra("EXPIRY")
                txtFoodExpiry.setText(expiryDate)
            }
            catch (exc: Exception)
            {}

            try
            {
                vegan = intent.getIntExtra("VEGAN",-1)
            }
            catch (exc: Exception)
            {}

            try
            {
                vegetarian = intent.getIntExtra("VEGETARIAN",-1)
            }
            catch (exc: Exception)
            {}

            try
            {
                ingredients = intent.getStringArrayExtra("INGREDIENTS")
            }
            catch (exc: Exception)
            {}

            try
            {
                traces = intent.getStringArrayExtra("TRACES")
            }
            catch (exc: Exception)
            {}

            if (ServiceHandler.vegan == true && vegan == 0){
                Toast.makeText(applicationContext,"Food is not vegan", Toast.LENGTH_LONG).show()
            }
            else if (ServiceHandler.vegan == true && vegan == 1){
                Toast.makeText(applicationContext,"Food is vegan", Toast.LENGTH_LONG).show()
            }
            else if (ServiceHandler.vegetarian == true && vegetarian == 0){
                Toast.makeText(applicationContext,"Food is not vegetarian", Toast.LENGTH_LONG).show()
            }
            else if (ServiceHandler.vegetarian == true && vegetarian == 1){
                Toast.makeText(applicationContext,"Food is vegetarian", Toast.LENGTH_LONG).show()
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
                    "AddFoodComplete",hashMapOf(
                       "kitchenId" to intent.getIntExtra("kitchenId", 0)
                       ,"name" to foodName, "quantity" to quantity
                       , "expiryDate" to ServiceHandler.serializeDate(expiryDate),
                       "vegan" to vegan, "vegetarian" to vegetarian,
                   "calories" to -1, "ingredients" to JSONArray(ingredients),
                   "traces" to JSONArray(traces)),this,
                    Response.Listener { response ->
                        val json = JSONObject(response.toString())
                        val kitchenId = json.getBoolean("AddFoodCompleteResult")
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

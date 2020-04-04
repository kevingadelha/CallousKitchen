package com.example.callouskitchenandroid

import android.app.DatePickerDialog
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.DatePicker
import android.widget.EditText
import android.widget.Toast
import com.android.volley.Response
import org.json.JSONObject
import org.json.JSONObject.NULL
import java.text.SimpleDateFormat
import java.time.LocalDate
import java.time.format.DateTimeFormatter
import java.time.format.DateTimeParseException
import java.util.*

class EditFoodActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_edit_food)

        val txtFoodName = findViewById<EditText>(R.id.editFoodName)
        val txtFoodQuantity = findViewById<EditText>(R.id.editFoodQuantity)
        val txtFoodExpiry = findViewById<EditText>(R.id.editFoodExpiry)
        val btnEditFood = findViewById<Button>(R.id.btnEditFoodItem)
        val btnCancel = findViewById<Button>(R.id.btnCancelEditFood)

        // populate the fields
        val food = intent.getSerializableExtra("FOOD") as Food

        txtFoodName.setText(food.name)
        txtFoodQuantity.setText(food.quantity.toString())



        var cal = Calendar.getInstance()

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

        btnEditFood.setOnClickListener{
            val foodName = txtFoodName.text.toString()

            if (foodName.isNullOrEmpty())
            {
                Toast.makeText(applicationContext,"Please enter the food name", Toast.LENGTH_LONG).show()
            }
            else {
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
                    "EditFood",hashMapOf("id" to food.id, "name" to foodName, "quantity" to quantity, "expiryDate" to ServiceHandler.serializeDate(expiryDate)),this,
                    Response.Listener { response ->

                        val json = JSONObject(response.toString())
                        val kitchensJson = json.getBoolean("EditFoodResult")
                        println(kitchensJson.toString())


                        val intent = Intent(this@EditFoodActivity, InventoryActivity::class.java)
                        startActivity(intent)
                    })
            }
        }

        btnCancel.setOnClickListener{
            val intent = Intent(this@EditFoodActivity, InventoryActivity::class.java)
            startActivity(intent)
        }


        txtFoodExpiry.setOnFocusChangeListener { v, hasFocus ->
            DatePickerDialog(this@EditFoodActivity,
                dateSetListener,
                // set DatePickerDialog to point to today's date when it loads up
                cal.get(Calendar.YEAR),
                cal.get(Calendar.MONTH),
                cal.get(Calendar.DAY_OF_MONTH)).show()
        }
    }
}

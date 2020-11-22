/* Authors: Kevin Gadelha, Laura Stewart
 *
 */
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
import kotlinx.android.synthetic.main.activity_edit_food.*
import kotlinx.android.synthetic.main.activity_kitchen_list.*
import kotlinx.android.synthetic.main.activity_kitchen_list.bottomNav
import org.json.JSONObject
import org.json.JSONObject.NULL
import java.text.SimpleDateFormat
import java.time.LocalDate
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter
import java.time.format.DateTimeParseException
import java.util.*

class EditFoodActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_edit_food)

        // set up bottom nav bar
        setNavigation()

        val txtFoodName = findViewById<EditText>(R.id.editFoodName)
        val txtFoodQuantity = findViewById<EditText>(R.id.editFoodQuantity)
        val txtFoodExpiry = findViewById<EditText>(R.id.editFoodExpiry)
        val btnEditFood = findViewById<Button>(R.id.btnEditFoodItem)
        val btnCancel = findViewById<Button>(R.id.btnCancelEditFood)

        // populate the fields
        val food = intent.getSerializableExtra("FOOD") as Food

        txtFoodName.setText(food.name)
        txtFoodQuantity.setText(food.quantity.toString())
       // var formatter = DateTimeFormatter.ofPattern("MM/dd/yyyy")
       // txtFoodExpiry.setText(food.expiryDate?.format(formatter))

        var cal = Calendar.getInstance()
        if (food.expiryDate != null){
            // Set date this way to make sure the month index is right
            cal.set(Calendar.YEAR, food.expiryDate?.year!!)
            cal.set(Calendar.MONTH, food.expiryDate?.monthValue!! - 1)
            cal.set(Calendar.DAY_OF_MONTH, food.expiryDate?.dayOfMonth!!)
            val myFormat = "MM/dd/yyyy"
            val sdf = SimpleDateFormat(myFormat, Locale.US)
            txtFoodExpiry.setText(sdf.format(cal.time))
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

        // The edit text must not be focusable (see xml file) for this to work
        editFoodExpiry.setOnClickListener{
            DatePickerDialog(this@EditFoodActivity, dateSetListener,
                cal.get(Calendar.YEAR),
                cal.get(Calendar.MONTH),
                cal.get(Calendar.DAY_OF_MONTH)).show()
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

                var expiryDate : LocalDate? = null
                if (!txtFoodExpiry.text.isNullOrEmpty()){
                    try{
                        expiryDate = LocalDate.parse(txtFoodExpiry.text.toString(), DateTimeFormatter.ofPattern("MM/dd/yyyy"))
                    }
                    catch(e : DateTimeParseException){
                        Toast.makeText(applicationContext,"Please enter a valid date", Toast.LENGTH_LONG).show()
                        return@setOnClickListener
                    }
                }

                ServiceHandler.callAccountService(
                    "EditFood",hashMapOf("id" to food.id, "name" to foodName, "quantity" to quantity, "expiryDate" to ServiceHandler.serializeDate(expiryDate)),this,
                    Response.Listener { response ->

                        val json = JSONObject(response.toString())
                        val success = json.getBoolean("EditFoodResult")
                        if (!success){
                            Toast.makeText(applicationContext,"Failed :(", Toast.LENGTH_LONG).show()
                        }

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
            if (hasFocus){
                val year = food.expiryDate?.year
                val month = food.expiryDate?.monthValue
                //No idea why but the datepicker's month is off by one
                val day = food.expiryDate?.dayOfMonth
                DatePickerDialog(this@EditFoodActivity,
                    dateSetListener,
                    // set DatePickerDialog to point to today's date when it loads up
                    year ?: cal.get(Calendar.YEAR),
                    month ?: cal.get(Calendar.MONTH),
                    day ?: cal.get(Calendar.DAY_OF_MONTH)).show()
            }
        }
    }

    /*
     * Override Android's default back button press
     */
    override fun onBackPressed() {
        // do nothing for now
        val intent = Intent(this@EditFoodActivity, InventoryActivity::class.java)
        startActivity(intent)
    }

    /*
     * Links the bottom navigation buttons to the correct activities
     */
    private fun setNavigation() {
        bottomNav.setOnNavigationItemSelectedListener {
            when (it.itemId){
                R.id.navigation_recipes -> {
                    // go to recipes
                    val intent = Intent(this@EditFoodActivity, RecipeSearchActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_inventory -> {
                    // go to the categories list
                    val intent = Intent(this@EditFoodActivity, KitchenListActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_settings -> {
                    // go to settings
                    val intent = Intent(this@EditFoodActivity, SettingsActivity::class.java)
                    startActivity(intent)
                    true
                }
                else -> false
            }
        }
    }
}

package com.example.callouskitchenandroid

import android.app.DatePickerDialog
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.*
import com.android.volley.Response
import kotlinx.android.synthetic.main.activity_kitchen_list.*
import org.json.JSONObject
import java.text.SimpleDateFormat
import java.time.LocalDate
import java.time.format.DateTimeFormatter
import java.time.format.DateTimeParseException
import java.util.*

class EatFoodActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_eat_food)

        // set up bottom nav bar
        setNavigation()

        val txtFoodName = findViewById<TextView>(R.id.textViewFoodTitleEat)
        val txtFoodQuantity = findViewById<EditText>(R.id.editEatFoodQuantity)
        val btnEatFood = findViewById<Button>(R.id.btnEatFoodItem)
        val btnCancel = findViewById<Button>(R.id.btnCancelEatFood)

        // populate the fields
        val food = intent.getSerializableExtra("FOOD") as Food

        txtFoodName.text = food.name
        txtFoodQuantity.setText(food.quantity.toString())

        btnEatFood.setOnClickListener{
            val quantityString = txtFoodQuantity.text.toString()
            if (!quantityString.isNullOrEmpty())
            {
                val quantity = quantityString.toDouble()

                ServiceHandler.callAccountService(
                    "EatFood",hashMapOf("id" to food.id, "quantity" to quantity),this,
                    Response.Listener { response ->

                        val json = JSONObject(response.toString())
                        val success = json.getBoolean("EatFoodResult")
                        if (!success){
                            Toast.makeText(applicationContext,"Failed :(", Toast.LENGTH_LONG).show()
                        }

                        val intent = Intent(this@EatFoodActivity, InventoryActivity::class.java)
                        startActivity(intent)
                    })
            }
            else
            {
                Toast.makeText(applicationContext,"Please enter a quantity", Toast.LENGTH_LONG).show()
            }

        }

        btnCancel.setOnClickListener{
            val intent = Intent(this@EatFoodActivity, InventoryActivity::class.java)
            startActivity(intent)
        }
    }

    private fun setNavigation() {
        bottomNav.setOnNavigationItemSelectedListener {
            when (it.itemId){
                R.id.navigation_recipes -> {
                    // go to recipes
                    val intent = Intent(this@EatFoodActivity, RecipeSearchActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_inventory -> {
                    // go to the categories list
                    val intent = Intent(this@EatFoodActivity, KitchenListActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_settings -> {
                    // go to settings
                    val intent = Intent(this@EatFoodActivity, SettingsActivity::class.java)
                    startActivity(intent)
                    true
                }
                else -> false
            }
        }
    }
}

package com.example.callouskitchenandroid

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import android.widget.Toast
import com.android.volley.Response
import org.json.JSONObject

class EatFoodActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_eat_food)

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
                        val kitchensJson = json.getBoolean("EatFoodResult")
                        println(kitchensJson.toString())


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
}

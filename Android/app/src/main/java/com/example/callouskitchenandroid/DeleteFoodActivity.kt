package com.example.callouskitchenandroid

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import android.widget.Toast
import com.android.volley.Response
import kotlinx.android.synthetic.main.activity_kitchen_list.*
import org.json.JSONObject

class DeleteFoodActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_delete_food)

        val txtFoodName = findViewById<TextView>(R.id.textViewFoodTitleDelete)
        val txtFoodQuantity = findViewById<EditText>(R.id.editDeleteFoodQuantity)
        val btnEatFood = findViewById<Button>(R.id.btnDeleteFoodItem)
        val btnCancel = findViewById<Button>(R.id.btnCancelDeleteFood)

        // populate the fields
        val food = intent.getSerializableExtra("FOOD") as Food

        txtFoodName.text = food.name
        txtFoodQuantity.setText(food.quantity.toString())

        btnEatFood.setOnClickListener{
            val quantityString = txtFoodQuantity.text.toString()
            if (!quantityString.isNullOrEmpty())
            {
                ServiceHandler.callAccountService(
                    "RemoveItem",hashMapOf("id" to food.id),this,
                    Response.Listener { response ->

                        val json = JSONObject(response.toString())
                        val kitchensJson = json.getBoolean("RemoveItemResult")
                        println(kitchensJson.toString())

                        val intent = Intent(this@DeleteFoodActivity, InventoryActivity::class.java)
                        startActivity(intent)
                    })

            }
            else
            {
                Toast.makeText(applicationContext,"Please enter a quantity", Toast.LENGTH_LONG).show()
            }

        }

        btnCancel.setOnClickListener{
            val intent = Intent(this@DeleteFoodActivity, InventoryActivity::class.java)
            startActivity(intent)
        }
    }
}

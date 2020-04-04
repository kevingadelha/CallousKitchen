package com.example.callouskitchenandroid

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast

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

        btnEditFood.setOnClickListener{
            val foodName = txtFoodName.text.toString()

            if (foodName.isNullOrEmpty())
            {
                Toast.makeText(applicationContext,"Please enter the food name", Toast.LENGTH_LONG).show()
            }
            else {
                // todo: edit the food with the service
                val quantityString = txtFoodQuantity.text.toString()
                var quantity: Double = 1.0
                if (!quantityString.isNullOrEmpty())
                    quantity = quantityString.toDouble()

                val intent = Intent(this@EditFoodActivity, InventoryActivity::class.java)
                startActivity(intent)
            }
        }

        btnCancel.setOnClickListener{
            val intent = Intent(this@EditFoodActivity, InventoryActivity::class.java)
            startActivity(intent)
        }
    }
}

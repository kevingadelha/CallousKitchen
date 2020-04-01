package com.example.callouskitchenandroid

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast

class AddKitchenActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_add_kitchen)

        val createButton = findViewById<Button>(R.id.btnCreateKitchen)
        val cancelButton = findViewById<Button>(R.id.btnCancelCreateKitchen)

        val txtKitchenName = findViewById<EditText>(R.id.editTextKitchenName)

        createButton.setOnClickListener(){
            // Validate the kitchen name
            if (!(txtKitchenName.text.isNullOrEmpty()))
            {
                // todo: add kitchen using web service

                // go to food inventory
                val intent = Intent(this@AddKitchenActivity, InventoryActivity::class.java)
                startActivity(intent)
            }
            else {
                Toast.makeText(applicationContext, "Please enter a kitchen name", Toast.LENGTH_LONG).show()
            }
        }

        cancelButton.setOnClickListener(){
            val intent = Intent(this@AddKitchenActivity, KitchenListActivity::class.java)
            startActivity(intent)
        }

    }
}

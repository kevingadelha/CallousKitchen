package com.example.callouskitchenandroid

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import com.android.volley.Response
import kotlinx.android.synthetic.main.activity_kitchen_list.*
import org.json.JSONObject

class AddKitchenActivity : AppCompatActivity() {

    //Important!!!! Don't use this for now, it is broken
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_add_kitchen)

        // set up bottom nav bar
        setNavigation()

        val createButton = findViewById<Button>(R.id.btnCreateKitchen)
        val cancelButton = findViewById<Button>(R.id.btnCancelCreateKitchen)

        val txtKitchenName = findViewById<EditText>(R.id.editTextKitchenName)

        createButton.setOnClickListener(){
            // Validate the kitchen name
            if (!(txtKitchenName.text.isNullOrEmpty()))
            {
                ServiceHandler.callAccountService(
                    "AddKitchen",hashMapOf("userId" to ServiceHandler.userId,"name" to txtKitchenName.text.toString()),this,
                    Response.Listener { response ->
                        val json = JSONObject(response.toString())
                        val kitchenId = json.getInt("AddKitchenResult")
                        // go to food inventory
                        val intent = Intent(this@AddKitchenActivity, InventoryActivity::class.java)
                        intent.putExtra("kitchenId",kitchenId)
                        startActivity(intent)

                    })
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


    private fun setNavigation() {
        bottomNav.setOnNavigationItemSelectedListener {
            when (it.itemId){
                R.id.navigation_recipes -> {
                    // go to recipes
                    val intent = Intent(this@AddKitchenActivity, RecipeSearchActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_inventory -> {
                    // go to the categories list
                    val intent = Intent(this@AddKitchenActivity, KitchenListActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_settings -> {
                    // go to settings
                    val intent = Intent(this@AddKitchenActivity, SettingsActivity::class.java)
                    startActivity(intent)
                    true
                }
                else -> false
            }
        }
    }
}

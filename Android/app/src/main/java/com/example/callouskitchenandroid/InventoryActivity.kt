package com.example.callouskitchenandroid

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.ViewGroup
import android.widget.Button
import com.android.volley.Response
import com.google.android.material.floatingactionbutton.FloatingActionButton
import kotlinx.android.synthetic.main.activity_inventory.*
import org.json.JSONObject

class InventoryActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_inventory)
        // set up bottom nav bar
        setNavigation()

        ServiceHandler.callAccountService(
            "GetInventory",hashMapOf("kitchenId" to ServiceHandler.primaryKitchenId),this,
            Response.Listener { response ->

                val json = JSONObject(response.toString())
                val foodsJson = json.getJSONArray("GetInventoryResult")
                var foods: ArrayList<Food> = arrayListOf<Food>()
                    for (i in 0 until foodsJson.length()) {
                        var foodJson: JSONObject = foodsJson.getJSONObject(i)
                        if (ServiceHandler.lastCategory == foodJson.getString("Storage")){
                            var food = Food(foodJson.getInt("Id"),foodJson.getString("Name"))
                            food.quantity = foodJson.getDouble("Quantity")
                            food.expiryDate = ServiceHandler.deSerializeDate(foodJson.getString("ExpiryDate"))
                            foods.add(food)
                        }

                    }
                // set the title
                title = ServiceHandler.lastCategory

                val foodListAdapter = FoodListAdapter(this, foods)
                val footerView = layoutInflater.inflate(R.layout.footer_view, listViewFood, false) as ViewGroup
                listViewFood.addFooterView(footerView)
                listViewFood.adapter = foodListAdapter
            })

        val btnAddFood = findViewById<FloatingActionButton>(R.id.btnAddFood)

        btnAddFood.setOnClickListener{
            val intent = Intent(this@InventoryActivity, AddFoodActivity::class.java)
            startActivity(intent)
        }

    }

    private fun setNavigation() {
        bottomNavInventory.setOnNavigationItemSelectedListener {
            when (it.itemId){
                R.id.navigation_recipes -> {
                    // go to recipes
                    val intent = Intent(this@InventoryActivity, RecipeSearchActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_inventory -> {
                    // go to the categories list
                    val intent = Intent(this@InventoryActivity, KitchenListActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_settings -> {
                    // go to settings
                    val intent = Intent(this@InventoryActivity, SettingsActivity::class.java)
                    startActivity(intent)
                    true
                }
                else -> false
            }
        }
    }
}

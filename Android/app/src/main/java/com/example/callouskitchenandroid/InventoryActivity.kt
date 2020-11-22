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
import java.time.LocalDate

class InventoryActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        //This intent comes from the notification
        if (intent.getBooleanExtra("Expiring Soon",false)){
            title = "Expiring Soon"
            //This might not be necessary but I want to make sure I don't break anything
            ServiceHandler.lastCategory = "Expiring Soon"
        }
        else{
            // set the title of the activity
            title = ServiceHandler.lastCategory
        }
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
                            food.quantityClassifier = foodJson.getString("QuantityClassifier")
                            food.expiryDate = ServiceHandler.deSerializeDate(foodJson.getString("ExpiryDate"))
                            food.favourite = foodJson.getBoolean("Favourite")
                            foods.add(food)
                        }
                        else if (ServiceHandler.lastCategory == "All"){
                            var food = Food(foodJson.getInt("Id"),foodJson.getString("Name"))
                            food.quantity = foodJson.getDouble("Quantity")
                            food.quantityClassifier = foodJson.getString("QuantityClassifier")
                            food.expiryDate = ServiceHandler.deSerializeDate(foodJson.getString("ExpiryDate"))
                            food.favourite = foodJson.getBoolean("Favourite")
                            foods.add(food)
                        }
                        else if (ServiceHandler.lastCategory == "Expiring Soon" || intent.getBooleanExtra("Expiring Soon",false)){
                            var food = Food(foodJson.getInt("Id"),foodJson.getString("Name"))
                            food.quantity = foodJson.getDouble("Quantity")
                            food.quantityClassifier = foodJson.getString("QuantityClassifier")
                            food.expiryDate = ServiceHandler.deSerializeDate(foodJson.getString("ExpiryDate"))
                            food.favourite = foodJson.getBoolean("Favourite")
                            if (food.expiryDate != null && (food.expiryDate!! < LocalDate.now().plusDays(3))){
                                foods.add(food)
                            }
                        }

                    }

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

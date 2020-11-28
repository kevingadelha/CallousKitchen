/* Authors: Kevin Gadelha, Laura Stewart
 *
 */
package com.example.callouskitchenandroid

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
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
            //If the user clicked the notification and is not logged in, get them to log in
            if (ServiceHandler.userId == -1){
                val intent = Intent(this, MainActivity::class.java)
                startActivity(intent)
            }
            title = "Expiring Soon"
            ServiceHandler.lastCategory = "Expiring Soon"
        }
        else{
            // set the title of the activity
            title = ServiceHandler.lastCategory
        }

        setContentView(R.layout.activity_inventory)
        // set up bottom nav bar
        setNavigation()

        val btnAddFood = findViewById<FloatingActionButton>(R.id.btnAddFood)

        //TODO: When categories can actually be edited add this button back in
        if (ServiceHandler.lastCategory == "All" || ServiceHandler.lastCategory == "Expiring Soon"){
            btnAddFood.visibility = View.GONE
        }
        else{
            btnAddFood.setOnClickListener{
                val intent = Intent(this@InventoryActivity, AddFoodActivity::class.java)
                startActivity(intent)
            }
        }

        ServiceHandler.callAccountService(
            "GetInventory",hashMapOf("kitchenId" to ServiceHandler.primaryKitchenId),this,
            Response.Listener { response ->

                val json = JSONObject(response.toString())
                val foodsJson = json.optJSONArray("GetInventoryResult")
                var foods: ArrayList<Food> = arrayListOf<Food>()
                    for (i in 0 until (foodsJson?.length() ?: 0)) {
                        var foodJson: JSONObject = foodsJson.getJSONObject(i)
                        if (ServiceHandler.lastCategory == foodJson.getString("Storage") || ServiceHandler.lastCategory == "Expiring Soon" || intent.getBooleanExtra("Expiring Soon",false) || ServiceHandler.lastCategory == "All"){
                            var food = Food(foodJson.getInt("Id"),foodJson.getString("Name"))
                            food.quantity = foodJson.getDouble("Quantity")
                            food.quantityClassifier = foodJson.getString("QuantityClassifier")
                            food.expiryDate = ServiceHandler.deSerializeDate(foodJson.getString("ExpiryDate"))
                            food.favourite = foodJson.getBoolean("Favourite")
                            var ingredientsArray = foodJson.getJSONArray("Ingredients")
                            food.ingredients =
                                Array<String>(ingredientsArray.length()) { "n = $it" }
                            for (i in 0 until (ingredientsArray.length())) {
                                food.ingredients[i] =
                                    ingredientsArray[i].toString()
                            }
                            var tracesArray = foodJson.getJSONArray("Traces")
                            food.traces =
                                Array<String>(tracesArray.length()) { "n = $it" }
                            for (i in 0 until (tracesArray.length())) {
                                food.traces[i] =
                                    tracesArray[i].toString()
                            }
                            food.vegan = foodJson.getInt("Vegan")
                            food.vegetarian = foodJson.getInt("Vegetarian")
                            foods.add(food)
                        }

                    }
                if (ServiceHandler.lastCategory == "Expiring Soon" || intent.getBooleanExtra("Expiring Soon",false)){
                    foods = ArrayList(foods.sortedWith(compareBy({ it.expiryDate })))
                }
                val foodListAdapter = FoodListAdapter(this, foods)
                val footerView = layoutInflater.inflate(R.layout.footer_view, listViewFood, false) as ViewGroup
                listViewFood.addFooterView(footerView)
                listViewFood.adapter = foodListAdapter
            })


    }

    /*
     * Override Android's default back button press
     */
    override fun onBackPressed() {
        // do nothing for now
        val intent = Intent(this@InventoryActivity, KitchenListActivity::class.java)
        startActivity(intent)
    }

    /*
     * Links the bottom navigation buttons to the correct activities
     */
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

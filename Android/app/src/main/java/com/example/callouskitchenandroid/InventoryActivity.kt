/* Authors: Kevin Gadelha, Laura Stewart
 *
 */
package com.example.callouskitchenandroid

import android.app.Activity
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.EditText
import android.widget.Spinner
import com.android.volley.Response
import com.google.android.material.floatingactionbutton.FloatingActionButton
import kotlinx.android.synthetic.main.activity_inventory.*
import org.json.JSONObject
import java.time.LocalDate

class InventoryActivity : AppCompatActivity() {
    var foods: ArrayList<Food> = arrayListOf<Food>()
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

        // TODO: implement search and sort
        val txtSearchInventory = findViewById<EditText>(R.id.searchFood)
        val spinnerSort = findViewById<Spinner>(R.id.spinnerSort)

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

        txtSearchInventory.addTextChangedListener(object : TextWatcher {

            override fun afterTextChanged(s: Editable) {}

            override fun beforeTextChanged(s: CharSequence, start: Int,
                                           count: Int, after: Int) {
            }

            override fun onTextChanged(s: CharSequence, start: Int,
                                       before: Int, count: Int) {
                if (s.isNotEmpty()){
                    val filteredFoods = foods.filter { food -> food.name.contains(s)  }
                    val foodListAdapter = FoodListAdapter(this@InventoryActivity, filteredFoods)
                    val footerView = layoutInflater.inflate(R.layout.footer_view, listViewFood, false) as ViewGroup
                    listViewFood.addFooterView(footerView)
                    listViewFood.adapter = foodListAdapter
                }
                else{
                    val foodListAdapter = FoodListAdapter(this@InventoryActivity, foods)
                    val footerView = layoutInflater.inflate(R.layout.footer_view, listViewFood, false) as ViewGroup
                    listViewFood.addFooterView(footerView)
                    listViewFood.adapter = foodListAdapter
                }
            }
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

package com.example.callouskitchenandroid

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.ViewGroup
import com.android.volley.Response
import kotlinx.android.synthetic.main.activity_inventory.*
import kotlinx.android.synthetic.main.activity_shopping_list.*
import org.json.JSONObject

class ShoppingListActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        // set the title of the activity
        title = ServiceHandler.lastCategory
        setContentView(R.layout.activity_shopping_list)
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
                        var food = Food(foodJson.getInt("Id"),foodJson.getString("Name"))
                        food.quantity = foodJson.getDouble("Quantity")
                        food.expiryDate = ServiceHandler.deSerializeDate(foodJson.getString("ExpiryDate"))
                        food.favourite = foodJson.getBoolean("Favourite")
                    if (food.favourite && food.quantity <= 3){
                        foods.add(food)
                    }

                }


                val shoppingListAdapter = ShoppingListAdapter(this, foods)
                listViewShoppingList.adapter = shoppingListAdapter
            })

    }

    private fun setNavigation() {
        bottomNavShoppingList.setOnNavigationItemSelectedListener {
            when (it.itemId){
                R.id.navigation_recipes -> {
                    // go to recipes
                    val intent = Intent(this@ShoppingListActivity, RecipeSearchActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_inventory -> {
                    // go to the categories list
                    val intent = Intent(this@ShoppingListActivity, KitchenListActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_settings -> {
                    // go to settings
                    val intent = Intent(this@ShoppingListActivity, SettingsActivity::class.java)
                    startActivity(intent)
                    true
                }
                else -> false
            }
        }
    }
}
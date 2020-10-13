package com.example.callouskitchenandroid

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import com.android.volley.Response
import kotlinx.android.synthetic.main.activity_inventory.*
import kotlinx.android.synthetic.main.activity_kitchen_list.*
import org.json.JSONObject

class InventoryActivity : AppCompatActivity() {
var kitchenId : Int = 0
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_inventory)

        // set up bottom nav bar
        setNavigation()

        //weird hack because the kitchenId was getting reset when going to inventory from add food
        kitchenId = intent.getIntExtra("kitchenId",ServiceHandler.lastKitchenId)
        ServiceHandler.lastKitchenId = kitchenId
        ServiceHandler.callAccountService(
            "GetInventory",hashMapOf("kitchenId" to kitchenId),this,
            Response.Listener { response ->

                val json = JSONObject(response.toString())
                val foodsJson = json.getJSONArray("GetInventoryResult")
                var foods: ArrayList<Food> = arrayListOf<Food>()

                    for (i in 0 until foodsJson.length()) {

                        var foodJson: JSONObject = foodsJson.getJSONObject(i)
                        var food = Food(foodJson.getInt("Id"),foodJson.getString("Name"))
                        food.quantity = foodJson.getDouble("Quantity")
                        food.expiryDate = ServiceHandler.deSerializeDate(foodJson.getString("ExpiryDate"))
                        foods.add(food)

                    }
                val foodListAdapter = FoodListAdapter(this, foods)
                listViewFood.adapter = foodListAdapter
            })

        //showAllFood()

        val btnAddFood = findViewById<Button>(R.id.btnAddFood)

        btnAddFood.setOnClickListener{
            val intent = Intent(this@InventoryActivity, AddFoodActivity::class.java)
            intent.putExtra("kitchenId",kitchenId)
            startActivity(intent)
        }

      /*  val btnKitchen = findViewById<Button>(R.id.btnOpenInventory)

        btnKitchen.setOnClickListener{
            val intent = Intent(this@InventoryActivity, KitchenListActivity::class.java)
            startActivity(intent)
        }*/
    }

    private fun showAllFood() {
        // test foods
        var foods: ArrayList<Food> = arrayListOf<Food>()
        foods.add(Food(1, "Banana", 2.0))
        foods.add(Food(2, "Chips", 1.0))

        val foodListAdapter = FoodListAdapter(this, foods)
        listViewFood.adapter = foodListAdapter
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

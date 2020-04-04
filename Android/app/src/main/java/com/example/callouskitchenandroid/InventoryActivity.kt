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

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_inventory)

        ServiceHandler.callAccountService(
            "GetInventory",hashMapOf("kitchenId" to intent.getIntExtra("kitchenId",0)),this,
            Response.Listener { response ->

                val json = JSONObject(response.toString())
                val foodsJson = json.getJSONArray("GetInventoryResult")
                var foods: ArrayList<Food> = arrayListOf<Food>()

                    for (i in 0 until foodsJson.length()) {

                        var foodJson: JSONObject = foodsJson.getJSONObject(i)
                        var food = Food(foodJson.getInt("Id"),foodJson.getString("Name"))
                        food.quantity = foodJson.getDouble("Quantity")
                        foods.add(food)

                    }



                val foodListAdapter = FoodListAdapter(this, foods)
                listViewFood.adapter = foodListAdapter
            })


        showAllFood()

        val btnAddFood = findViewById<Button>(R.id.btnAddFood)

        btnAddFood.setOnClickListener{
            val intent = Intent(this@InventoryActivity, AddFoodActivity::class.java)
            startActivity(intent)
        }
    }

    private fun showAllFood() {
        // test foods
        var foods: ArrayList<Food> = arrayListOf<Food>()
        foods.add(Food(1, "Banana"))
        foods.add(Food(2, "Chips"))

        // todo: actually get the food from the database

        val foodListAdapter = FoodListAdapter(this, foods)
        listViewFood.adapter = foodListAdapter
    }
}

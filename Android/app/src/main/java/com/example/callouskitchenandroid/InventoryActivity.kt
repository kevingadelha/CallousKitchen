package com.example.callouskitchenandroid

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import kotlinx.android.synthetic.main.activity_inventory.*

class InventoryActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_inventory)

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
        foods.add(Food(1, "Banana", 2.0))
        foods.add(Food(2, "Chips", 1.0))

        // todo: actually get the food from the database

        val foodListAdapter = FoodListAdapter(this, foods)
        listViewFood.adapter = foodListAdapter
    }
}

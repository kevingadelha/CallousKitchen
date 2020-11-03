package com.example.callouskitchenandroid

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import kotlinx.android.synthetic.main.activity_inventory.*
import kotlinx.android.synthetic.main.activity_shopping_list.*

class ShoppingListActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_shopping_list)

        var foods: ArrayList<Food> = arrayListOf<Food>()
        val shoppingListAdapter = ShoppingListAdapter(this, foods)
        listViewShoppingList.adapter = shoppingListAdapter
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
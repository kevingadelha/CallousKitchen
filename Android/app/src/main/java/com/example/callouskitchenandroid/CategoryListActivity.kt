package com.example.callouskitchenandroid

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import kotlinx.android.synthetic.main.activity_category_list.*
import kotlinx.android.synthetic.main.activity_category_list.bottomNav

class CategoryListActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_category_list)

        // set the bottom nav bar
        setNavigation()

        // TODO: get the list of categories in the kitchen using the web service

        //temporary test code
        var categories: ArrayList<Category> = ArrayList<Category>()
        categories.add(Category(1, "Pantry", 1))
        categories.add(Category(2, "Fridge", 1))
        val categoryListAdapter = CategoryListAdapter(this, categories)
        listViewCategory.adapter = categoryListAdapter

    }

    // TODO: change the navigation in all classes to go to categories instead of kitchens (once categories are functional)
    private fun setNavigation() {
        bottomNav.setOnNavigationItemSelectedListener {
            when (it.itemId){
                R.id.navigation_recipes -> {
                    // go to recipes
                    val intent = Intent(this@CategoryListActivity, RecipeSearchActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_inventory -> {
                    // stay in the inventory
                    true
                }
                R.id.navigation_settings -> {
                    // go to settings
                    val intent = Intent(this@CategoryListActivity, SettingsActivity::class.java)
                    startActivity(intent)
                    true
                }
                else -> false
            }
        }
    }
}
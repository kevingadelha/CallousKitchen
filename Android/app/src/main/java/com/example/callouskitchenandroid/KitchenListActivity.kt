package com.example.callouskitchenandroid

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.Toast
import com.android.volley.Response
import kotlinx.android.synthetic.main.activity_kitchen_list.*
import org.json.JSONObject

class KitchenListActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_kitchen_list)

        // set the bottom nav bar
        setNavigation()

        var kitchens: ArrayList<Kitchen> = arrayListOf<Kitchen>()
        kitchens.add(Kitchen(0,"Fridge"))
        kitchens.add(Kitchen(1,"Freezer"))
        kitchens.add(Kitchen(2,"Pantry"))
        kitchens.add(Kitchen(3,"Cupboard"))
        kitchens.add(Kitchen(4,"Cellar"))
        kitchens.add(Kitchen(5,"Other"))
        val kitchenListAdapter = KitchenListAdapter(this, kitchens)
        listView.adapter = kitchenListAdapter

        // Get add button
        val btnAddKitchen = findViewById<Button>(R.id.btnAddKitchen)

        //TODO: Incorporate some way to manage categories maybe
        btnAddKitchen.setOnClickListener(){
            // go to add kitchen view
            //val intent = Intent(this@KitchenListActivity, AddKitchenActivity::class.java)
            //startActivity(intent)
        }
    }

    private fun setNavigation() {
        bottomNav.setOnNavigationItemSelectedListener {
            when (it.itemId){
                R.id.navigation_recipes -> {
                    // go to recipes
                    val intent = Intent(this@KitchenListActivity, RecipeSearchActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_inventory -> {
                    // stay in the inventory
                    true
                }
                R.id.navigation_settings -> {
                    // go to settings
                    val intent = Intent(this@KitchenListActivity, SettingsActivity::class.java)
                    startActivity(intent)
                    true
                }
                else -> false
            }
        }
    }
}

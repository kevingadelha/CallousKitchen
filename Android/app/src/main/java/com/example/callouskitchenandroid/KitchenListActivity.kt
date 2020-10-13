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

        //This totally won't work yet
        ServiceHandler.callAccountService(
            "GetKitchens",hashMapOf("userId" to ServiceHandler.userId),this,
            Response.Listener { response ->

                val json = JSONObject(response.toString())
                val kitchensJson = json.getJSONArray("GetKitchensResult")
                var kitchens: ArrayList<Kitchen> = arrayListOf<Kitchen>()

                for (i in 0 until kitchensJson.length()) {

                    var kitchenJson: JSONObject = kitchensJson.getJSONObject(i)
                    kitchens.add(Kitchen(kitchenJson.getInt("Id"),kitchenJson.getString("Name")))

                }
                val kitchenListAdapter = KitchenListAdapter(this, kitchens)
                listView.adapter = kitchenListAdapter
            })
        //showAllKitchens()

        // Get add button
        val btnAddKitchen = findViewById<Button>(R.id.btnAddKitchen)

        btnAddKitchen.setOnClickListener(){
            // go to add kitchen view
            val intent = Intent(this@KitchenListActivity, AddKitchenActivity::class.java)
            startActivity(intent)
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

    private fun showAllKitchens() {
        // test kitchens
        var kitchens: ArrayList<Kitchen> = ArrayList<Kitchen>()
        kitchens.add(Kitchen(1, "Home Kitchen"))
        kitchens.add(Kitchen(2, "New Kitchen"))



        val kitchenListAdapter = KitchenListAdapter(this, kitchens)
        listView.adapter = kitchenListAdapter
    }
}

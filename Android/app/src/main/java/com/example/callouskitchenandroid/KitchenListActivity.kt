package com.example.callouskitchenandroid

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import kotlinx.android.synthetic.main.activity_kitchen_list.*

class KitchenListActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_kitchen_list)

        showAllKitchens()

        // Get add button
        val btnAddKitchen = findViewById<Button>(R.id.btnAddKitchen)

        btnAddKitchen.setOnClickListener(){
            // go to add kitchen view
            val intent = Intent(this@KitchenListActivity, AddKitchenActivity::class.java)
            startActivity(intent)
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

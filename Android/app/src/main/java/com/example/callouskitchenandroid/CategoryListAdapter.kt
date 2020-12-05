/* Author: Laura Stewart */
package com.example.callouskitchenandroid

import android.app.Activity
import android.content.Intent
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.Button

class CategoryListAdapter (private val context: Activity,
                           private val categories: List<Category>)
    : ArrayAdapter<Category>(context, R.layout.kitchen_list, categories)
{
    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View {
        val inflater = context.layoutInflater
        val rowView = inflater.inflate(R.layout.kitchen_list, null, true)

        // change the button text to match the kitchen name
        val kitchenBtn = rowView.findViewById<Button>(R.id.btnKitchen)
        kitchenBtn.text = categories[position].name

        // set on click listener for the button to go to the inventory view
        kitchenBtn.setOnClickListener(){
            val host = kitchenBtn.context as Activity
            if (categories[position].name != "Shopping List")
            {
                val intent = Intent(host, InventoryActivity::class.java)
                ServiceHandler.lastCategory = categories[position].name
                host.startActivity(intent)
            }
            else
            {
                val intent = Intent(host, ShoppingListActivity::class.java)
                ServiceHandler.lastCategory = categories[position].name
                host.startActivity(intent)
            }

        }

        return rowView
    }
}
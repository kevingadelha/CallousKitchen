package com.example.callouskitchenandroid

import android.app.Activity
import android.content.Intent
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.Button

class CategoryListAdapter (private val context: Activity,
                           private val categories: List<Category>)
    : ArrayAdapter<Category>(context, R.layout.category_list, categories) {

    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View {
        val inflater = context.layoutInflater
        val rowView = inflater.inflate(R.layout.category_list, null, true)

        // change the button text to match the kitchen name
        val categoryBtn = rowView.findViewById<Button>(R.id.btnCategory)
        categoryBtn.text = categories[position].name

        // set on click listener for the button to go to the inventory view
        categoryBtn.setOnClickListener(){
            val host = categoryBtn.context as Activity
            val intent = Intent(host, InventoryActivity::class.java)
            intent.putExtra("categoryId", categories[position].id)
            intent.putExtra("kitchenId",categories[position].kitchenId)
            host.startActivity(intent)
        }

        return rowView
    }


}
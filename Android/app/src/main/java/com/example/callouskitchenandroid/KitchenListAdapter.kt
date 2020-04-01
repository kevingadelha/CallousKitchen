package com.example.callouskitchenandroid

import android.R
import android.app.Activity
import android.content.Intent
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.Button


class KitchenListAdapter (private val context: Activity,
                          private val kitchens: List<Kitchen>)
    : ArrayAdapter<Kitchen>(context, com.example.callouskitchenandroid.R.layout.kitchen_list, kitchens)
{
    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View {
        val inflater = context.layoutInflater
        val rowView = inflater.inflate(com.example.callouskitchenandroid.R.layout.kitchen_list, null, true)

        // change the button text to match the kitchen name
        val kitchenBtn = rowView.findViewById<Button>(com.example.callouskitchenandroid.R.id.btnKitchen)
        kitchenBtn.text = kitchens[position].name

        // set on click listener for the button to go to the inventory view
        kitchenBtn.setOnClickListener(){
            val host = kitchenBtn.getContext() as Activity
            val intent = Intent(host, InventoryActivity::class.java)
            host.startActivity(intent)
        }

        return rowView
    }
}
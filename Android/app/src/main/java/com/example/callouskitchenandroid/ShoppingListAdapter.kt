package com.example.callouskitchenandroid

import android.app.Activity
import android.view.View
import android.view.ViewGroup
import android.widget.*

class ShoppingListAdapter(private val context: Activity,
                          private val foods: List<Food>)
    : ArrayAdapter<Food>(context, R.layout.food_item_list, foods) {

    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View {
        val inflater = context.layoutInflater
        val rowView = inflater.inflate(R.layout.shopping_list_item, null, true)

        // change the text to match the food name
        val chkBxFoodName = rowView.findViewById<CheckBox>(R.id.checkBoxFood)
        chkBxFoodName.text = foods[position].name

        return rowView
    }
}
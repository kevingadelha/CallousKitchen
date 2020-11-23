/*
 * Author: Laura Stewart
 */
package com.example.callouskitchenandroid

import android.app.Activity
import android.content.SharedPreferences
import android.view.View
import android.view.ViewGroup
import android.widget.*

class ShoppingListAdapter(private val context: Activity,
                          private val foods: List<Food>)
    : ArrayAdapter<Food>(context, R.layout.food_item_list, foods) {

    private val sharedPref: SharedPreferences = context.getSharedPreferences("ShoppingList", 0)

    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View {
        val inflater = context.layoutInflater
        val rowView = inflater.inflate(R.layout.shopping_list_item, null, true)

        // change the text to match the food name
        val chkBxFoodName = rowView.findViewById<CheckBox>(R.id.checkBoxFood)
        chkBxFoodName.text = foods[position].name

        // Check shared preferences to see if this checkbox should be checked
        chkBxFoodName.isChecked = sharedPref.getBoolean(foods[position].name, false)

        // When the check box value is changed, update the shared preferences
        chkBxFoodName.setOnCheckedChangeListener { buttonView, isChecked ->
            if (chkBxFoodName.isChecked) {
                // store in Shared Preferences
                with(sharedPref.edit()) {
                    putBoolean(foods[position].name, true)
                    apply()
                }
            }
            else
            {
                // delete from shared preferences
                with(sharedPref.edit()) {
                    remove(foods[position].name)
                    apply()
                }
            }
        }

        return rowView
    }
}
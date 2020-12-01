/* Author: Laura Stewart */
package com.example.callouskitchenandroid

import android.app.Activity
import android.content.SharedPreferences
import android.view.View
import android.view.ViewGroup
import android.widget.*
import com.android.volley.Response
import org.json.JSONObject

class ShoppingListAdapter(private val context: Activity,
                          private val foods: List<Food>)
    : ArrayAdapter<Food>(context, R.layout.food_item_list, foods) {

    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View {
        val inflater = context.layoutInflater
        val rowView = inflater.inflate(R.layout.shopping_list_item, null, true)

        // change the text to match the food name
        val chkBxFoodName = rowView.findViewById<CheckBox>(R.id.checkBoxFood)
        chkBxFoodName.text = foods[position].name

        // Check shared preferences to see if this checkbox should be checked
        chkBxFoodName.isChecked = foods[position].onShoppingList

        // When the check box value is changed, update the shared preferences
        chkBxFoodName.setOnCheckedChangeListener { buttonView, isChecked ->
            ServiceHandler.callAccountService(
                "ShoppingListFood", hashMapOf(
                    "foodId" to foods[position].id,
                    "onShoppingList" to isChecked
                ), context,
                Response.Listener { response ->
                    val json = JSONObject(response.toString())
                    val success = json.getBoolean("ShoppingListFoodResult")
                    // return to the food list
                    if (!success){
                        Toast.makeText(context,"Failed :(", Toast.LENGTH_LONG).show()
                        //Undo if fail
                        chkBxFoodName.isChecked = !isChecked
                    }
                    else{
                        //Update the entry in the original list
                        foods[position].onShoppingList = isChecked
                    }
                })
        }

        // Show the food quantity
        val txtQuantity = rowView.findViewById<TextView>(R.id.textViewShopQuantity)
        txtQuantity.text = "Quantity remaining: ${foods[position].quantity} ${foods[position].quantityClassifier}"

        return rowView
    }
}
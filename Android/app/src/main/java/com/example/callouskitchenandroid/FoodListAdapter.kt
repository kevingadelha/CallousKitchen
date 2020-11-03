package com.example.callouskitchenandroid

import android.app.Activity
import android.content.Intent
import android.graphics.Color
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.ImageButton
import android.widget.TextView
import android.widget.Toast
import androidx.core.content.ContextCompat
import com.android.volley.Response
import org.json.JSONArray
import org.json.JSONObject
import java.time.LocalDate

class FoodListAdapter(private val context: Activity,
                      private val foods: List<Food>)
    : ArrayAdapter<Food>(context, R.layout.food_item_list, foods) {

    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View {
        val inflater = context.layoutInflater
        val rowView = inflater.inflate(R.layout.food_item_list, null, true)

        // change the text to match the food name
        val txtFood = rowView.findViewById<TextView>(R.id.textViewFoodTitle)
        //txtFood.text = foods[position].name + " x" + foods[position].quantity + " expires on " + foods[position].expiryDate?.toString()
        txtFood.text = foods[position].name

        // add expiry date and quantity
        val txtQuantity = rowView.findViewById<TextView>(R.id.textViewFoodQuantity)
        txtQuantity.text = "Quantity: " + foods[position].quantity

        val txtExpiry = rowView.findViewById<TextView>(R.id.textViewExpiry)

        // Compare dates to change colour
        val currentDate = LocalDate.now()
        val expiryDate = foods[position].expiryDate
        val expiryMinusThree = expiryDate?.minusDays(3) // for checking if an item is about to expire
        if (expiryDate!! < currentDate) // food is expired
        {
            txtExpiry.text = "EXPIRED: " + foods[position].expiryDate?.toString()
            txtExpiry.setTextColor(ContextCompat.getColor(context, R.color.redText))
        }
        else if (expiryMinusThree!! < currentDate)
        {
            txtExpiry.text = "EXPIRES SOON: " + foods[position].expiryDate?.toString()
            txtExpiry.setTextColor(ContextCompat.getColor(context, R.color.orangeText))
        }
        else
        {
            txtExpiry.text = "Expires on: " + foods[position].expiryDate?.toString()
            txtExpiry.setTextColor(ContextCompat.getColor(context, R.color.whiteText))
        }

        // set on click events for edit, eat, and delete
        val btnEdit = rowView.findViewById<ImageButton>(R.id.imgBtnEditFood)
        val btnEat = rowView.findViewById<ImageButton>(R.id.imgBtnEatFood)
        val btnDelete = rowView.findViewById<ImageButton>(R.id.imgBtnDeleteFood)
        val btnFavourite = rowView.findViewById<ImageButton>(R.id.imgBtnFavourite)

        btnEdit.setOnClickListener{
            // open edit activity
            val host = btnEdit.context as Activity
            val intent = Intent(host, EditFoodActivity::class.java)

            // send food data
            intent.putExtra("FOOD", foods[position])

            host.startActivity(intent)
        }

        btnEat.setOnClickListener{
            // open eat activity
            val host = btnEat.context as Activity
            val intent = Intent(host, EatFoodActivity::class.java)

            // send food data
            intent.putExtra("FOOD", foods[position])

            host.startActivity(intent)
        }

        btnDelete.setOnClickListener{
            // open delete activity
            val host = btnDelete.context as Activity
            val intent = Intent(host, DeleteFoodActivity::class.java)

            // send food data
            intent.putExtra("FOOD", foods[position])

            host.startActivity(intent)
        }

        var isFavourite = foods[position].favourite
        if (isFavourite)
            btnFavourite.setImageResource(R.drawable.filledstar)
        else
            btnFavourite.setImageResource(R.drawable.star)
        var foodId = foods[position].id

        btnFavourite.setOnClickListener{

            isFavourite = !isFavourite
            ServiceHandler.callAccountService(
                "FavouriteFood", hashMapOf(
                    "foodId" to foodId,
                    "favourite" to isFavourite
                ), context,
                Response.Listener { response ->
                    val json = JSONObject(response.toString())
                    val success = json.getBoolean("FavouriteFoodResult")
                    // return to the food list
                    if (!success){
                        Toast.makeText(context,"Failed :(", Toast.LENGTH_LONG).show()
                        isFavourite = !isFavourite
                    }
                    else{
                        if (isFavourite)
                            btnFavourite.setImageResource(R.drawable.filledstar)
                        else
                            btnFavourite.setImageResource(R.drawable.star)
                    }
                })
        }

        return rowView
    }
}
/* Author: Laura Stewart */
package com.example.callouskitchenandroid

import android.app.Activity
import android.content.Intent
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.ImageView
import android.widget.TextView
import com.squareup.picasso.Picasso

class RecipeListAdapter (private val context: Activity,
                         private val recipes: List<Recipe>)
    : ArrayAdapter<Recipe>(context, R.layout.recipe_item_list, recipes) {

    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View {
        val inflater = context.layoutInflater
        val rowView = inflater.inflate(R.layout.recipe_item_list, null, true)

        val txtRecipeTitle = rowView.findViewById<TextView>(R.id.textViewRecipeTitle)
        val txtRecipeYield = rowView.findViewById<TextView>(R.id.textViewRecipeYield)
        val txtRecipeSource = rowView.findViewById<TextView>(R.id.textViewRecipeSource)
        val imgRecipe  = rowView.findViewById<ImageView>(R.id.imageViewRecipe)

        txtRecipeTitle.text = recipes[position].name
        txtRecipeYield.text = "Servings: " + recipes[position].recipeYield
        txtRecipeSource.text = recipes[position].source
        val url = recipes[position].image
        if (!url.isNullOrEmpty()) {
            Picasso.get().load(url).into(imgRecipe)
        }

        rowView.setOnClickListener {
            // open recipe activity
            val host = rowView.context as Activity
            val intent = Intent(host, RecipeViewActivity::class.java)
            intent.putExtra("URL", recipes[position].url)
            host.startActivity(intent)

        }

        return rowView
    }

}
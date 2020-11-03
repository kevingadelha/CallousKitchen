package com.example.callouskitchenandroid

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.webkit.WebView

class RecipeViewActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_recipe_view)

        val recipeDisplay = findViewById<WebView>(R.id.webviewRecipe)

        // example
        recipeDisplay.loadUrl("https://www.allrecipes.com/recipe/260540/chef-johns-sourdough-bread/")

    }
}
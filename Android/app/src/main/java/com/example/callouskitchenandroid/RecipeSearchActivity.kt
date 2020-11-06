package com.example.callouskitchenandroid

import android.app.SearchManager
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.SearchView
import kotlinx.android.synthetic.main.activity_kitchen_list.bottomNav
import kotlinx.android.synthetic.main.activity_recipe_search.*

class RecipeSearchActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_recipe_search)

        // set up bottom nav bar
        setNavigation()

        val searchBar = findViewById<SearchView>(R.id.searchViewRecipes)

        // TODO: I have to figure out some xml configurations
        if (Intent.ACTION_SEARCH == intent.action)
        {
            intent.getStringExtra(SearchManager.QUERY)?.also  {
                query -> searchRecipes(query)
            }
        }

        val btnGetSuggestions = findViewById<Button>(R.id.btnGetRecipeSuggestions)

        btnGetSuggestions.setOnClickListener {

            // TODO: Search for expired ingredients
            // params: string search, int count, List<string> diets, int kitchenId

            // test code
            var recipes: ArrayList<Recipe> = arrayListOf<Recipe>()
            recipes.add(Recipe("Chili", "https://www.budgetbytes.com/basic-chili/", "Budget Bites", "", 6))
            val recipeListAdapter = RecipeListAdapter(this, recipes)
            listViewRecipe.adapter = recipeListAdapter
        }
    }

    private fun searchRecipes(query: String)
    {
        // search based on the ingredient entered in the search bar

        // present the results in the list view
    }

    private fun setNavigation() {
        bottomNav.setOnNavigationItemSelectedListener {
            when (it.itemId){
                R.id.navigation_recipes -> {
                    // stay in recipes
                    true
                }
                R.id.navigation_inventory -> {
                    // go to the categories list
                    val intent = Intent(this@RecipeSearchActivity, KitchenListActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_settings -> {
                    // go to settings
                    val intent = Intent(this@RecipeSearchActivity, SettingsActivity::class.java)
                    startActivity(intent)
                    true
                }
                else -> false
            }
        }
    }
}
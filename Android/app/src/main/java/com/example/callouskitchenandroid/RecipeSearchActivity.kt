/* Authors: Kevin Gadelha, Laura Stewart
 *
 */

package com.example.callouskitchenandroid

import android.app.SearchManager
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.ViewGroup
import android.widget.Button
import android.widget.SearchView
import com.android.volley.Response
import com.google.gson.JsonObject
import kotlinx.android.synthetic.main.activity_inventory.*
import kotlinx.android.synthetic.main.activity_kitchen_list.bottomNav
import kotlinx.android.synthetic.main.activity_recipe_search.*
import org.json.JSONArray
import org.json.JSONObject

class RecipeSearchActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_recipe_search)

        // set up bottom nav bar
        setNavigation()

        val footerView = layoutInflater.inflate(R.layout.recipe_footer_view, listViewFood, false) as ViewGroup
        listViewRecipe.addFooterView(footerView)

        val searchBar = findViewById<SearchView>(R.id.searchViewRecipes)

        searchBar.setOnQueryTextListener(object : SearchView.OnQueryTextListener {
            override fun onQueryTextSubmit(query: String): Boolean {
                searchRecipes(query)
                return false
            }
            override fun onQueryTextChange(newText: String): Boolean {
                return false
            }
        })

        val btnGetSuggestions = findViewById<Button>(R.id.btnGetRecipeSuggestions)

        btnGetSuggestions.setOnClickListener {
            ServiceHandler.callAccountService(
                "FeelingLuckyUser", hashMapOf(
                    "count" to 100,
                    "userId" to ServiceHandler.userId
                ), this,
                Response.Listener { response ->
                    val json = JSONObject(response.toString())
                    val recipesArray = json.optJSONArray("FeelingLuckyUserResult")
                    var recipes: ArrayList<Recipe> = arrayListOf<Recipe>()
                    for (i in 0 until (recipesArray?.length() ?: 0)) {
                        val recipe = recipesArray[i]!! as JSONObject
                        val name = recipe.optString("Name")
                        val url = recipe.optString("Url")
                        val source = recipe.optString("Source")
                        val image = recipe.optString("Image")
                        val recipeYield = recipe.optDouble("Yield")
                        recipes.add(Recipe(name,url,source,image,recipeYield))
                    }
                    val recipeListAdapter = RecipeListAdapter(this, recipes)
                    listViewRecipe.adapter = recipeListAdapter
                })
        }
    }

    private fun searchRecipes(query: String)
    {
        ServiceHandler.callAccountService(
            "SearchRecipesUser", hashMapOf(
                "search" to query,
                "count" to 100,
                "userId" to ServiceHandler.userId
            ), this,
            Response.Listener { response ->
                val json = JSONObject(response.toString())
                val recipesArray = json.optJSONArray("SearchRecipesUserResult")
                var recipes: ArrayList<Recipe> = arrayListOf<Recipe>()
                for (i in 0 until (recipesArray?.length() ?: 0)) {
                    val recipe = recipesArray[i]!! as JSONObject
                    val name = recipe.optString("Name")
                    val url = recipe.optString("Url")
                    val source = recipe.optString("Source")
                    val image = recipe.optString("Image")
                    val recipeYield = recipe.optDouble("Yield")
                    recipes.add(Recipe(name,url,source,image,recipeYield))
                }
                val recipeListAdapter = RecipeListAdapter(this, recipes)
                listViewRecipe.adapter = recipeListAdapter
            })
    }

    /*
     * Override Android's default back button press
     */
    override fun onBackPressed() {
        // do nothing for now
    }

    /*
     * Links the bottom navigation buttons to the correct activities
     */
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
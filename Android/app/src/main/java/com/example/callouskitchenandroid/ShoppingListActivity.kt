/* Authors: Kevin Gadelha, Laura Stewart */
package com.example.callouskitchenandroid

import android.content.Intent
import android.content.SharedPreferences
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import android.widget.EditText
import android.widget.Spinner
import com.android.volley.Response
import kotlinx.android.synthetic.main.activity_inventory.*
import kotlinx.android.synthetic.main.activity_recipe_search.*
import kotlinx.android.synthetic.main.activity_shopping_list.*
import org.json.JSONObject

class ShoppingListActivity : AppCompatActivity() {

    private val sharedPref: SharedPreferences = ServiceHandler.sharedPref

    var foods: ArrayList<Food> = arrayListOf<Food>()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        // set the title of the activity
        title = ServiceHandler.lastCategory
        setContentView(R.layout.activity_shopping_list)
        // set up bottom nav bar
        setNavigation()

        // set the footer for the list of food
        val footerView = layoutInflater.inflate(R.layout.footer_view, listViewShoppingList, false) as ViewGroup
        listViewShoppingList.addFooterView(footerView)

        ServiceHandler.callAccountService(
            "GetInventory",hashMapOf("kitchenId" to ServiceHandler.primaryKitchenId),this,
            Response.Listener { response ->

                val json = JSONObject(response.toString())
                val foodsJson = json.getJSONArray("GetInventoryResult")
                for (i in 0 until foodsJson.length()) {
                    var foodJson: JSONObject = foodsJson.getJSONObject(i)
                        var food = Food(foodJson.getInt("Id"),foodJson.getString("Name"))
                        food.quantity = foodJson.getDouble("Quantity")
                        food.quantityClassifier = foodJson.getString("QuantityClassifier")
                        food.expiryDate = ServiceHandler.deSerializeDate(foodJson.getString("ExpiryDate"))
                        food.favourite = foodJson.getBoolean("Favourite")
                        foods.add(food)

                }

                //Sorting twice basically accomplishes grouping
                //Sort by lowest quantity first
                foods = ArrayList(foods.sortedWith(compareBy({ it.quantity })))
                //Then make sure to show all the favourited foods first
                foods = ArrayList(foods.sortedWith(compareByDescending({ it.favourite })))
                val shoppingListAdapter = ShoppingListAdapter(this, foods)
                listViewShoppingList.adapter = shoppingListAdapter

            })

        val txtSearchShopping = findViewById<EditText>(R.id.searchShoppingList)
        val spinnerSort = findViewById<Spinner>(R.id.spinnerSortShopping)
        // Populate the Sorting spinner
        val sortingArray = resources.getStringArray(R.array.sortingOptionsShopping)
        val adapter = ArrayAdapter(this, R.layout.custom_spinner_item, sortingArray)
        spinnerSort.adapter = adapter
        spinnerSort.setSelection(sharedPref.getInt("lastIndexShopping", 0))
        txtSearchShopping.setText(sharedPref.getString("lastSearchShopping", ""))

        // detect value change in the search bar and update the filtering
        txtSearchShopping.addTextChangedListener(object : TextWatcher {

            override fun afterTextChanged(s: Editable) {}

            override fun beforeTextChanged(s: CharSequence, start: Int,
                                           count: Int, after: Int) {
            }

            override fun onTextChanged(s: CharSequence, start: Int,
                                       before: Int, count: Int) {
                with(sharedPref.edit()) {
                    putString("lastSearchShopping", s.toString())
                    apply()
                }
                updateSortedAndFilteredList()
            }
        })

        // detect selection change in the spinner and sort the list
        spinnerSort.onItemSelectedListener = object : AdapterView.OnItemSelectedListener {
            override fun onItemSelected(
                parent: AdapterView<*>,
                view: View,
                position: Int,
                id: Long
            ) {
                with(sharedPref.edit()) {
                    putInt("lastIndexShopping", position)
                    apply()
                }
                updateSortedAndFilteredList()
            }

            override fun onNothingSelected(parent: AdapterView<*>?) {
                // another interface callback
            }
        }

    }

    /*
     * Sort the shopping list based on the dropdown
     */
    private fun updateSortedAndFilteredList(){
        val txtSearchShopping = findViewById<EditText>(R.id.searchShoppingList)
        val spinnerSort = findViewById<Spinner>(R.id.spinnerSortShopping)
        var sort = spinnerSort.selectedItem.toString()
        when (sort) {
            "Default" -> {
                //Sort by lowest quantity first
                foods = ArrayList(foods.sortedWith(compareBy({ it.quantity })))
                //Then make sure to show all the favourited foods first
                foods = ArrayList(foods.sortedWith(compareByDescending({ it.favourite })))
            }
            "Recently Added" -> foods = ArrayList(foods.sortedWith(compareByDescending ({ it.id })))
            "Expiring Soon" -> foods = ArrayList(foods.sortedWith(Comparator<Food>{ a, b ->
                when {
                    a.expiryDate == null && b.expiryDate != null -> 1
                    a.expiryDate != null && b.expiryDate == null -> -1
                    a.expiryDate == null && b.expiryDate == null -> 0
                    a.expiryDate!! > b.expiryDate!! -> 1
                    a.expiryDate!! < b.expiryDate!! -> -1
                    else -> 0
                }
            }))
            "Running Low" -> foods = ArrayList(foods.sortedWith(compareBy({ it.quantity })))
            "Favourited" -> foods = ArrayList(foods.sortedWith(compareByDescending({ it.favourite })))
            "Oldest Added" -> foods = ArrayList(foods.sortedWith(compareBy({ it.id })))
            "Alphabetical" -> foods = ArrayList(foods.sortedWith(compareBy({ it.name })))
            "Greatest Quantity" -> foods = ArrayList(foods.sortedWith(compareByDescending ({ it.quantity })))
            "Quantity Type" -> foods = ArrayList(foods.sortedWith(compareBy({ it.quantityClassifier })))
        }
        if (txtSearchShopping.text.isNotEmpty()){
            val filteredFoods = foods.filter { food -> food.name.contains(txtSearchShopping.text)  }
            val shoppingListAdapter = ShoppingListAdapter(this@ShoppingListActivity, filteredFoods)
            listViewShoppingList.adapter = shoppingListAdapter
        }
        else{
            val shoppingListAdapter = ShoppingListAdapter(this@ShoppingListActivity, foods)
            listViewShoppingList.adapter = shoppingListAdapter
        }
    }

    /*
     * Override Android's default back button press
     */
    override fun onBackPressed() {
        // go back to the category list
        val intent = Intent(this@ShoppingListActivity, KitchenListActivity::class.java)
        startActivity(intent)
    }

    /*
     * Links the bottom navigation buttons to the correct activities
     */
    private fun setNavigation() {
        bottomNavShoppingList.setOnNavigationItemSelectedListener {
            when (it.itemId){
                R.id.navigation_recipes -> {
                    // go to recipes
                    val intent = Intent(this@ShoppingListActivity, RecipeSearchActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_inventory -> {
                    // go to the categories list
                    val intent = Intent(this@ShoppingListActivity, KitchenListActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_settings -> {
                    // go to settings
                    val intent = Intent(this@ShoppingListActivity, SettingsActivity::class.java)
                    startActivity(intent)
                    true
                }
                else -> false
            }
        }
    }
}
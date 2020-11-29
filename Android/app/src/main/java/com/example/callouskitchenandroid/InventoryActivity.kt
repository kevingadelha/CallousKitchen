/* Authors: Kevin Gadelha, Laura Stewart
 *
 */
package com.example.callouskitchenandroid

import android.app.Activity
import android.content.Intent
import android.content.SharedPreferences
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.view.View
import android.view.ViewGroup
import android.widget.*
import com.android.volley.Response
import com.google.android.material.floatingactionbutton.FloatingActionButton
import kotlinx.android.synthetic.main.activity_inventory.*
import org.json.JSONObject
import java.time.LocalDate

class InventoryActivity : AppCompatActivity() {
    var foods: ArrayList<Food> = arrayListOf<Food>()
    private val sharedPref: SharedPreferences = ServiceHandler.sharedPref
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        //This intent comes from the notification
        if (intent.getBooleanExtra("Expiring Soon",false)){
            //If the user clicked the notification and is not logged in, get them to log in
            if (ServiceHandler.userId == -1){
                val intent = Intent(this, MainActivity::class.java)
                startActivity(intent)
            }
            title = "Expiring Soon"
            //If the user clicked the expiring soon notification, select expiring soon for sort
            //1 is the index for expiring soon
            with(sharedPref.edit()) {
                putInt("lastIndex", 1)
                apply()
            }
            //And show everything
            ServiceHandler.lastCategory = "All"
        }
        else{
            // set the title of the activity
            title = ServiceHandler.lastCategory
        }

        setContentView(R.layout.activity_inventory)
        // set up bottom nav bar
        setNavigation()

        // set the footer for the food list
        val footerView = layoutInflater.inflate(R.layout.footer_view, listViewFood, false) as ViewGroup
        listViewFood.addFooterView(footerView)

        val txtSearchInventory = findViewById<EditText>(R.id.searchFood)
        val spinnerSort = findViewById<Spinner>(R.id.spinnerSort)
        // Populate the Sorting spinner
        val sortingArray = resources.getStringArray(R.array.sortingOptions)
        val adapter = ArrayAdapter(this, R.layout.custom_spinner_item, sortingArray)
        spinnerSort.adapter = adapter
        spinnerSort.setSelection(sharedPref.getInt("lastIndex", 0))
        txtSearchInventory.setText(sharedPref.getString("lastSearch", ""))

        val btnAddFood = findViewById<FloatingActionButton>(R.id.btnAddFood)

            btnAddFood.setOnClickListener{
                val intent = Intent(this@InventoryActivity, AddFoodActivity::class.java)
                startActivity(intent)
            }

        ServiceHandler.callAccountService(
            "GetInventory",hashMapOf("kitchenId" to ServiceHandler.primaryKitchenId),this,
            Response.Listener { response ->

                val json = JSONObject(response.toString())
                val foodsJson = json.optJSONArray("GetInventoryResult")
                    for (i in 0 until (foodsJson?.length() ?: 0)) {
                        var foodJson: JSONObject = foodsJson.getJSONObject(i)
                        if (ServiceHandler.lastCategory == foodJson.getString("Storage") || ServiceHandler.lastCategory == "All"){
                            var food = Food(foodJson.getInt("Id"),foodJson.getString("Name"))
                            food.quantity = foodJson.getDouble("Quantity")
                            food.quantityClassifier = foodJson.getString("QuantityClassifier")
                            food.storage = foodJson.getString("Storage")
                            food.expiryDate = ServiceHandler.deSerializeDate(foodJson.getString("ExpiryDate"))
                            food.favourite = foodJson.getBoolean("Favourite")
                            var ingredientsArray = foodJson.getJSONArray("Ingredients")
                            food.ingredients =
                                Array<String>(ingredientsArray.length()) { "n = $it" }
                            for (i in 0 until (ingredientsArray.length())) {
                                food.ingredients[i] =
                                    ingredientsArray[i].toString()
                            }
                            var tracesArray = foodJson.getJSONArray("Traces")
                            food.traces =
                                Array<String>(tracesArray.length()) { "n = $it" }
                            for (i in 0 until (tracesArray.length())) {
                                food.traces[i] =
                                    tracesArray[i].toString()
                            }
                            food.vegan = foodJson.getInt("Vegan")
                            food.vegetarian = foodJson.getInt("Vegetarian")
                            foods.add(food)
                        }

                    }

                updateSortedAndFilteredList()
            })

        txtSearchInventory.addTextChangedListener(object : TextWatcher {

            override fun afterTextChanged(s: Editable) {}

            override fun beforeTextChanged(s: CharSequence, start: Int,
                                           count: Int, after: Int) {
            }

            override fun onTextChanged(s: CharSequence, start: Int,
                                       before: Int, count: Int) {
                with(sharedPref.edit()) {
                    putString("lastSearch", s.toString())
                    apply()
                }
                updateSortedAndFilteredList()
            }
        })
        spinnerSort.onItemSelectedListener = object : AdapterView.OnItemSelectedListener {
            override fun onItemSelected(
                parent: AdapterView<*>,
                view: View,
                position: Int,
                id: Long
            ) {
                with(sharedPref.edit()) {
                    putInt("lastIndex", position)
                    apply()
                }
                updateSortedAndFilteredList()
            }

            override fun onNothingSelected(parent: AdapterView<*>?) {
                // another interface callback
            }
        }
    }

    private fun updateSortedAndFilteredList(){
        val txtSearchInventory = findViewById<EditText>(R.id.searchFood)
        val spinnerSort = findViewById<Spinner>(R.id.spinnerSort)
        var sort = spinnerSort.getSelectedItem().toString();
        when (sort) {
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
        if (txtSearchInventory.text.isNotEmpty()){
            val filteredFoods = foods.filter { food -> food.name.contains(txtSearchInventory.text)  }
            val foodListAdapter = FoodListAdapter(this@InventoryActivity, filteredFoods)
            listViewFood.adapter = foodListAdapter
        }
        else{
            val foodListAdapter = FoodListAdapter(this@InventoryActivity, foods)
            listViewFood.adapter = foodListAdapter
        }
    }

    /*
     * Override Android's default back button press
     */
    override fun onBackPressed() {
        // do nothing for now
        val intent = Intent(this@InventoryActivity, KitchenListActivity::class.java)
        startActivity(intent)
    }

    /*
     * Links the bottom navigation buttons to the correct activities
     */
    private fun setNavigation() {
        bottomNavInventory.setOnNavigationItemSelectedListener {
            when (it.itemId){
                R.id.navigation_recipes -> {
                    // go to recipes
                    val intent = Intent(this@InventoryActivity, RecipeSearchActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_inventory -> {
                    // go to the categories list
                    val intent = Intent(this@InventoryActivity, KitchenListActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_settings -> {
                    // go to settings
                    val intent = Intent(this@InventoryActivity, SettingsActivity::class.java)
                    startActivity(intent)
                    true
                }
                else -> false
            }
        }
    }
}

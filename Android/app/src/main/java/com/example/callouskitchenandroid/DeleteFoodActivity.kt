/* Authors: Kevin Gadelha, Laura Stewart
 *
 */

package com.example.callouskitchenandroid

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.*
import com.android.volley.Response
import kotlinx.android.synthetic.main.activity_kitchen_list.*
import org.json.JSONObject

class DeleteFoodActivity : AppCompatActivity() {

    // The number of steps in the food quantity slider
    private val SLIDER_MAX = 10

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_delete_food)

        // set up bottom nav bar
        setNavigation()

        val txtFoodName = findViewById<TextView>(R.id.textViewFoodTitleDelete)
        val btnDeleteFood = findViewById<Button>(R.id.btnDeleteFoodItem)
        val btnCancel = findViewById<Button>(R.id.btnCancelDeleteFood)

        // Slider and text representation
        val seekBarQuantity = findViewById<SeekBar>(R.id.seekBarDeleteFood)
        val txtViewQuantity = findViewById<TextView>(R.id.textViewDeleteQuantity)

        // populate the fields
        val food = intent.getSerializableExtra("FOOD") as Food

        txtFoodName.text = food.name

        var units = food.quantityClassifier

        // Set the seekbar max to 10 so there will be 10 "steps" in the bar
        // food quantity will be calculated using percentages
        seekBarQuantity.max = SLIDER_MAX
        seekBarQuantity.progress = SLIDER_MAX

        txtViewQuantity.text = "${food.quantity} $units"

        // detect changes in seek bar value
        seekBarQuantity.setOnSeekBarChangeListener(object : SeekBar.OnSeekBarChangeListener {
            override fun onProgressChanged(seekBar: SeekBar, progress: Int, fromUser: Boolean) {
                // convert seekbar's current value to a percent
                val percent = progress.toDouble() / SLIDER_MAX.toDouble()

                // calculate the amount of food remaining
                val remainingQuantity = food.quantity * percent

                txtViewQuantity.text = "$remainingQuantity $units"
            }
            override fun onStartTrackingTouch(seekBar: SeekBar) {}
            override fun onStopTrackingTouch(seekBar: SeekBar) {}
        })

        btnDeleteFood.setOnClickListener{
            // calculate the amount of food remaining
            val remainingQuantity = food.quantity * (seekBarQuantity.progress.toDouble() / SLIDER_MAX.toDouble())

            val quantityString = remainingQuantity.toString()
            if (!quantityString.isNullOrEmpty())
            {
                ServiceHandler.callAccountService(
                    "RemoveItem",hashMapOf("id" to food.id),this,
                    Response.Listener { response ->

                        val json = JSONObject(response.toString())
                        val success = json.getBoolean("RemoveItemResult")
                        if (!success){
                            Toast.makeText(applicationContext,"Failed :(", Toast.LENGTH_LONG).show()
                        }

                        val intent = Intent(this@DeleteFoodActivity, InventoryActivity::class.java)
                        startActivity(intent)
                    })

            }
            else
            {
                Toast.makeText(applicationContext,"Please enter a quantity", Toast.LENGTH_LONG).show()
            }

        }

        btnCancel.setOnClickListener{
            val intent = Intent(this@DeleteFoodActivity, InventoryActivity::class.java)
            startActivity(intent)
        }
    }

    /*
     * Override Android's default back button press
     */
    override fun onBackPressed() {
        // do nothing for now
        val intent = Intent(this@DeleteFoodActivity, InventoryActivity::class.java)
        startActivity(intent)
    }

    /*
     * Links the bottom navigation buttons to the correct activities
     */
    private fun setNavigation() {
        bottomNav.setOnNavigationItemSelectedListener {
            when (it.itemId){
                R.id.navigation_recipes -> {
                    // go to recipes
                    val intent = Intent(this@DeleteFoodActivity, RecipeSearchActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_inventory -> {
                    // go to the categories list
                    val intent = Intent(this@DeleteFoodActivity, KitchenListActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_settings -> {
                    // go to settings
                    val intent = Intent(this@DeleteFoodActivity, SettingsActivity::class.java)
                    startActivity(intent)
                    true
                }
                else -> false
            }
        }
    }
}

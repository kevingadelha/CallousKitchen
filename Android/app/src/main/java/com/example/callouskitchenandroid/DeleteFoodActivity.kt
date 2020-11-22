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

        var units = "g" // temp

        // Set the seek bar max to the current quantity of food
        seekBarQuantity.max = food.quantity.toInt()
        seekBarQuantity.progress = food.quantity.toInt()

        txtViewQuantity.text = "${seekBarQuantity.progress} $units"

        // detect changes in seek bar value
        seekBarQuantity.setOnSeekBarChangeListener(object : SeekBar.OnSeekBarChangeListener {
            override fun onProgressChanged(seekBar: SeekBar, progress: Int, fromUser: Boolean) {
                // get the seekbar's current value and display it as text
                txtViewQuantity.text = "$progress $units"
            }
            override fun onStartTrackingTouch(seekBar: SeekBar) {}
            override fun onStopTrackingTouch(seekBar: SeekBar) {}
        })

        btnDeleteFood.setOnClickListener{
            val quantityString = seekBarQuantity.progress.toString()
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

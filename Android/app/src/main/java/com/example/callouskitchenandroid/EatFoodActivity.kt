/* Authors: Kevin Gadelha, Laura Stewart
 *
 */

package com.example.callouskitchenandroid

import android.app.DatePickerDialog
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.*
import com.android.volley.Response
import kotlinx.android.synthetic.main.activity_kitchen_list.*
import org.json.JSONObject
import java.text.SimpleDateFormat
import java.time.LocalDate
import java.time.format.DateTimeFormatter
import java.time.format.DateTimeParseException
import java.util.*

class EatFoodActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_eat_food)

        // set up bottom nav bar
        setNavigation()

        val txtFoodName = findViewById<TextView>(R.id.textViewFoodTitleEat)
        val btnEatFood = findViewById<Button>(R.id.btnEatFoodItem)
        val btnCancel = findViewById<Button>(R.id.btnCancelEatFood)

        // Slider and text representation
        val seekBarQuantity = findViewById<SeekBar>(R.id.seekBarEatFood)
        val txtViewQuantity = findViewById<TextView>(R.id.textViewQuantity)

        // populate the fields
        val food = intent.getSerializableExtra("FOOD") as Food

        txtFoodName.text = food.name

        var units = food.quantityClassifier

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

        btnEatFood.setOnClickListener{
            val quantityString = seekBarQuantity.progress.toString()
            if (!quantityString.isNullOrEmpty())
            {
                val quantity = quantityString.toDouble()

                ServiceHandler.callAccountService(
                    "EatFood",hashMapOf("id" to food.id, "quantity" to quantity),this,
                    Response.Listener { response ->

                        val json = JSONObject(response.toString())
                        val success = json.getBoolean("EatFoodResult")
                        if (!success){
                            Toast.makeText(applicationContext,"Failed :(", Toast.LENGTH_LONG).show()
                        }

                        val intent = Intent(this@EatFoodActivity, InventoryActivity::class.java)
                        startActivity(intent)
                    })
            }
            else
            {
                Toast.makeText(applicationContext,"Please enter a quantity", Toast.LENGTH_LONG).show()
            }

        }

        btnCancel.setOnClickListener{
            val intent = Intent(this@EatFoodActivity, InventoryActivity::class.java)
            startActivity(intent)
        }
    }

    /*
     * Links the bottom navigation buttons to the correct activities
     */
    private fun setNavigation() {
        bottomNav.setOnNavigationItemSelectedListener {
            when (it.itemId){
                R.id.navigation_recipes -> {
                    // go to recipes
                    val intent = Intent(this@EatFoodActivity, RecipeSearchActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_inventory -> {
                    // go to the categories list
                    val intent = Intent(this@EatFoodActivity, KitchenListActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_settings -> {
                    // go to settings
                    val intent = Intent(this@EatFoodActivity, SettingsActivity::class.java)
                    startActivity(intent)
                    true
                }
                else -> false
            }
        }
    }
}

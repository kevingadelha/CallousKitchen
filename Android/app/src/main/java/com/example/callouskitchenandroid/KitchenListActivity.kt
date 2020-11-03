package com.example.callouskitchenandroid

import android.app.Notification
import android.app.NotificationChannel
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.ViewGroup
import android.widget.Button
import android.widget.Toast
import androidx.core.app.NotificationCompat
import com.android.volley.Response
import com.google.android.material.floatingactionbutton.FloatingActionButton
import kotlinx.android.synthetic.main.activity_inventory.*
import kotlinx.android.synthetic.main.activity_kitchen_list.*
import org.json.JSONObject
import android.app.NotificationManager
import android.app.PendingIntent
import android.content.Context
import android.graphics.BitmapFactory
import android.graphics.Color
import android.os.Build
import android.widget.RemoteViews
import androidx.core.app.NotificationManagerCompat
import androidx.core.content.ContextCompat
import java.time.LocalDate
import java.time.LocalDateTime

class KitchenListActivity : AppCompatActivity() {

    //declaring variables
    lateinit var notificationManager : NotificationManager

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_kitchen_list)

        // set the bottom nav bar
        setNavigation()

        var kitchens: ArrayList<Kitchen> = arrayListOf<Kitchen>()
        kitchens.add(Kitchen(0,"Fridge"))
        kitchens.add(Kitchen(1,"Freezer"))
        kitchens.add(Kitchen(2,"Pantry"))
        kitchens.add(Kitchen(3,"Cupboard"))
        kitchens.add(Kitchen(4,"Cellar"))
        kitchens.add(Kitchen(5,"Other"))
        val kitchenListAdapter = KitchenListAdapter(this, kitchens)
        val footerView = layoutInflater.inflate(R.layout.footer_view, listView, false) as ViewGroup
        listView.addFooterView(footerView)
        listView.adapter = kitchenListAdapter

        // Get add button
        val btnAddKitchen = findViewById<FloatingActionButton>(R.id.btnAddKitchen)

        //TODO: Incorporate some way to manage categories maybe
        btnAddKitchen.setOnClickListener(){
            // go to add kitchen view
            //val intent = Intent(this@KitchenListActivity, AddKitchenActivity::class.java)
            //startActivity(intent)
        }

        ServiceHandler.callAccountService(
            "GetInventory",hashMapOf("kitchenId" to ServiceHandler.primaryKitchenId),this,
            Response.Listener { response ->
                val json = JSONObject(response.toString())
                val foodsJson = json.getJSONArray("GetInventoryResult")
                var foods: ArrayList<Food> = arrayListOf<Food>()
                var expiringFoods: ArrayList<String> = arrayListOf<String>()
                for (i in 0 until foodsJson.length()) {
                    var foodJson: JSONObject = foodsJson.getJSONObject(i)
                        var food = Food(foodJson.getInt("Id"),foodJson.getString("Name"))
                        food.quantity = foodJson.getDouble("Quantity")
                        food.expiryDate = ServiceHandler.deSerializeDate(foodJson.getString("ExpiryDate"))
                        food.favourite = foodJson.getBoolean("Favourite")
                        foods.add(food)
                        println(food.expiryDate != null)
                        println(food.expiryDate!! < LocalDate.now().plusDays(3))
                        if (food.expiryDate != null && (food.expiryDate!! < LocalDate.now().plusDays(3))){
                            expiringFoods.add(food.name)
                        }
                }
                createNotificationChannel()
                // creating the notification and its parameters.!
                val builder = NotificationCompat.Builder(this, "primary_notification_channel").apply {
                    setSmallIcon(R.drawable.hippo)
                    setContentTitle("Expiring")
                    setContentText(expiringFoods.joinToString {  it -> it  })
                    setPriority(NotificationCompat.PRIORITY_DEFAULT)
                }

                // displaying the notification with NotificationManagerCompat.
                with(NotificationManagerCompat.from(this)) {
                    notify(112, builder.build())
                }

            })
    }

    private fun createNotificationChannel() {
        notificationManager = getSystemService(Context.NOTIFICATION_SERVICE) as NotificationManager

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            val channel = NotificationChannel(
                "primary_notification_channel",
                "Messages",
                NotificationManager.IMPORTANCE_HIGH
            )

            channel.enableLights(true)
            channel.lightColor = Color.RED
            channel.enableVibration(true)
            channel.description = "Messages Notification"
            notificationManager.createNotificationChannel(channel)
        }
    }

    private fun setNavigation() {
        bottomNav.setOnNavigationItemSelectedListener {
            when (it.itemId){
                R.id.navigation_recipes -> {
                    // go to recipes
                    val intent = Intent(this@KitchenListActivity, RecipeSearchActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_inventory -> {
                    // stay in the inventory
                    true
                }
                R.id.navigation_settings -> {
                    // go to settings
                    val intent = Intent(this@KitchenListActivity, SettingsActivity::class.java)
                    startActivity(intent)
                    true
                }
                else -> false
            }
        }
    }
}

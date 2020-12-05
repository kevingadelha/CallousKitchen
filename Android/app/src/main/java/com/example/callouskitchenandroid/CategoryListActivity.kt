/* Authors: Kevin Gadelha, Laura Stewart */
package com.example.callouskitchenandroid

import android.app.NotificationChannel
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.ViewGroup
import kotlinx.android.synthetic.main.activity_inventory.*
import kotlinx.android.synthetic.main.activity_kitchen_list.*
import android.app.NotificationManager
import android.content.Context
import android.graphics.Color
import android.os.Build

class CategoryListActivity : AppCompatActivity() {

    //declaring variables
    lateinit var notificationManager : NotificationManager

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_kitchen_list)

        // set the bottom nav bar
        setNavigation()

        var categories: ArrayList<Category> = arrayListOf<Category>()
        categories.add(Category(0,"All"))
        categories.add(Category(1,"Fridge"))
        categories.add(Category(2,"Freezer"))
        categories.add(Category(3,"Pantry"))
        categories.add(Category(4,"Cupboard"))
        categories.add(Category(5,"Cellar"))
        categories.add(Category(6,"Other"))
        categories.add(Category(7, "Shopping List"))
        val categoryListAdapter = CategoryListAdapter(this, categories)
        val footerView = layoutInflater.inflate(R.layout.footer_view, listView, false) as ViewGroup
        listView.addFooterView(footerView)
        listView.adapter = categoryListAdapter

        // Get add button
  /*      val btnAddKitchen = findViewById<FloatingActionButton>(R.id.btnAddKitchen)

        btnAddKitchen.setOnClickListener(){
            // go to add kitchen view
            //val intent = Intent(this@KitchenListActivity, AddKitchenActivity::class.java)
            //startActivity(intent)
        }*/
//Don't do this here anymore
        /*
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
                        if (food.expiryDate != null && (food.expiryDate!! < LocalDate.now().plusDays(3))){
                            expiringFoods.add(food.name)
                        }
                }
                if (expiringFoods.size > 0){
                    createNotificationChannel()
                    // creating the notification and its parameters.!

                    // Create an explicit intent for an Activity in your app
                    val intent = Intent(this, InventoryActivity::class.java).apply {
                        flags = Intent.FLAG_ACTIVITY_NEW_TASK or Intent.FLAG_ACTIVITY_CLEAR_TASK
                    }
                    intent.putExtra("Expiring Soon",true)
                    val pendingIntent: PendingIntent = PendingIntent.getActivity(this, 0, intent, PendingIntent.FLAG_UPDATE_CURRENT)

                    val builder = NotificationCompat.Builder(this, "primary_notification_channel").apply {
                        setSmallIcon(R.drawable.hippo)
                        setContentTitle("Expiring")
                        setContentText(expiringFoods.joinToString {  it -> it  })
                        setPriority(NotificationCompat.PRIORITY_DEFAULT)
                        setContentIntent(pendingIntent)
                        setStyle(NotificationCompat.BigTextStyle().bigText(expiringFoods.joinToString {  it -> it  }))
                    }

                    // displaying the notification with NotificationManagerCompat.
                    with(NotificationManagerCompat.from(this)) {
                        notify(112, builder.build())
                    }
                }

            })*/
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
                    // go to recipes
                    val intent = Intent(this@CategoryListActivity, RecipeSearchActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_inventory -> {
                    // stay in the inventory
                    true
                }
                R.id.navigation_settings -> {
                    // go to settings
                    val intent = Intent(this@CategoryListActivity, SettingsActivity::class.java)
                    startActivity(intent)
                    true
                }
                else -> false
            }
        }
    }
}

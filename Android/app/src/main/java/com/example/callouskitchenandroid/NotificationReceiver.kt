package com.example.callouskitchenandroid

import android.app.NotificationChannel
import android.app.NotificationManager
import android.content.BroadcastReceiver
import android.content.Context
import android.content.Intent
import android.graphics.Color
import android.os.Build
import androidx.core.content.ContextCompat.getSystemService
import android.app.Notification
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
import android.app.PendingIntent
import android.graphics.BitmapFactory
import android.widget.RemoteViews
import androidx.core.app.NotificationManagerCompat
import androidx.core.content.ContextCompat
import java.time.LocalDate
import java.time.LocalDateTime

class NotificationReceiver : BroadcastReceiver() {
    //declaring variables
    lateinit var notificationManager: NotificationManager
    override fun onReceive(context: Context?, intent: Intent?) {
//Only do the notification if the user is logged in and this magical context I'm getting from somewhere isn't null
        if (ServiceHandler.userId != -1 && context != null) {

            ServiceHandler.callAccountService(
                "GetInventory", hashMapOf("kitchenId" to ServiceHandler.primaryKitchenId), context!!,
                Response.Listener { response ->
                    val json = JSONObject(response.toString())
                    val foodsJson = json.getJSONArray("GetInventoryResult")
                    var foods: ArrayList<Food> = arrayListOf<Food>()
                    var expiringFoods: ArrayList<String> = arrayListOf<String>()
                    for (i in 0 until foodsJson.length()) {
                        var foodJson: JSONObject = foodsJson.getJSONObject(i)
                        var food = Food(foodJson.getInt("Id"), foodJson.getString("Name"))
                        food.quantity = foodJson.getDouble("Quantity")
                        food.expiryDate =
                            ServiceHandler.deSerializeDate(foodJson.getString("ExpiryDate"))
                        food.favourite = foodJson.getBoolean("Favourite")
                        foods.add(food)
                        if (food.expiryDate != null && (food.expiryDate!! < LocalDate.now()
                                .plusDays(3))
                        ) {
                            expiringFoods.add(food.name)
                        }
                    }
                    if (expiringFoods.size > 0) {


                        notificationManager =
                            context?.getSystemService(Context.NOTIFICATION_SERVICE) as NotificationManager
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
                        // creating the notification and its parameters.!

                        // Create an explicit intent for an Activity in your app
                        val intent = Intent(context, InventoryActivity::class.java).apply {
                            flags = Intent.FLAG_ACTIVITY_NEW_TASK or Intent.FLAG_ACTIVITY_CLEAR_TASK
                        }
                        intent.putExtra("Expiring Soon", true)
                        val pendingIntent: PendingIntent = PendingIntent.getActivity(
                            context,
                            0,
                            intent,
                            PendingIntent.FLAG_UPDATE_CURRENT
                        )

                        val builder =
                            NotificationCompat.Builder(context, "primary_notification_channel").apply {
                                setSmallIcon(R.drawable.hippo)
                                setContentTitle("Expiring")
                                setContentText(expiringFoods.joinToString { it -> it })
                                setPriority(NotificationCompat.PRIORITY_DEFAULT)
                                setContentIntent(pendingIntent)
                                setStyle(
                                    NotificationCompat.BigTextStyle()
                                        .bigText(expiringFoods.joinToString { it -> it })
                                )
                            }

                        // displaying the notification with NotificationManagerCompat.
                        with(NotificationManagerCompat.from(context)) {
                            notify(112, builder.build())
                        }
                    }
                })
        }
    }
}

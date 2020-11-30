/* Author: Kevin Gadelha */
package com.example.callouskitchenandroid

import android.app.*
import android.content.Context
import android.content.Intent
import android.graphics.Color
import android.os.Build
import android.os.Handler
import android.os.IBinder
import android.util.Log
import androidx.core.app.NotificationCompat
import androidx.core.app.NotificationManagerCompat
import com.android.volley.Response
import org.json.JSONObject
import java.time.LocalDate
import java.util.*


class NotificationService : Service() {
    lateinit var notificationManager: NotificationManager
    override fun onBind(arg0: Intent): IBinder? {
        return null
    }

    override fun onStartCommand(intent: Intent, flags: Int, startId: Int): Int {
        super.onStartCommand(intent, flags, startId)
var context = applicationContext
//Only do the notification if the user is logged in and this magical context I'm getting from somewhere isn't null
        if (ServiceHandler.userId != -1 && context != null) {

            ServiceHandler.callAccountService(
                "GetInventory", hashMapOf("kitchenId" to ServiceHandler.primaryKitchenId), context!!,
                Response.Listener { response ->
                    val json = JSONObject(response.toString())
                    val foodsJson = json.getJSONArray("GetInventoryResult")
                    var foods: ArrayList<Food> = arrayListOf<Food>()
                    for (i in 0 until foodsJson.length()) {
                        var foodJson: JSONObject = foodsJson.getJSONObject(i)
                        var food = Food(foodJson.getInt("Id"), foodJson.getString("Name"))
                        food.expiryDate =
                            ServiceHandler.deSerializeDate(foodJson.getString("ExpiryDate"))
                        foods.add(food)
                    }
                    var expiringFoods = foods.filter { food -> food.expiryDate != null && (food.expiryDate!! < LocalDate.now()
                        .plusDays(3)) }
                    //Show expiring soonest first
                    expiringFoods = expiringFoods.sortedWith(Comparator<Food>{ a, b ->
                        when {
                            a.expiryDate == null && b.expiryDate != null -> 1
                            a.expiryDate != null && b.expiryDate == null -> -1
                            a.expiryDate == null && b.expiryDate == null -> 0
                            a.expiryDate!! > b.expiryDate!! -> 1
                            a.expiryDate!! < b.expiryDate!! -> -1
                            else -> 0
                        }
                    })
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
                            this.flags = Intent.FLAG_ACTIVITY_NEW_TASK or Intent.FLAG_ACTIVITY_CLEAR_TASK
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
                                setContentText(expiringFoods.joinToString { it -> it.name })
                                setPriority(NotificationCompat.PRIORITY_DEFAULT)
                                setContentIntent(pendingIntent)
                                setStyle(
                                    NotificationCompat.BigTextStyle()
                                        .bigText(expiringFoods.joinToString { it -> it.name })
                                )
                            }

                        // displaying the notification with NotificationManagerCompat.
                        with(NotificationManagerCompat.from(context)) {
                            notify(112, builder.build())
                        }
                    }
                })
        }
        return START_STICKY
    }
}
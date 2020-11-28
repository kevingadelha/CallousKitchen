package com.example.callouskitchenandroid

import android.app.AlarmManager
import android.app.PendingIntent
import android.content.Context
import android.content.Intent
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.android.volley.Response
import org.json.JSONObject
import java.util.*
import kotlin.collections.ArrayList


class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        myAlarm()

        ServiceHandler.sharedPref = getPreferences(Context.MODE_PRIVATE)

        var userId = ServiceHandler.sharedPref.getInt("userId", -1)
        if (userId != -1){
            ServiceHandler.userId = userId
            ServiceHandler.email = ServiceHandler.sharedPref.getString("email", null)
            ServiceHandler.vegan = ServiceHandler.sharedPref.getBoolean("vegan", false)
            ServiceHandler.vegetarian = ServiceHandler.sharedPref.getBoolean("vegetarian", false)
            val allergies = ServiceHandler.sharedPref.getStringSet("allergies", null)?.toMutableList()
            if (allergies != null){
                ServiceHandler.allergies = ArrayList(allergies!!)
            }
            ServiceHandler.primaryKitchenId = ServiceHandler.sharedPref.getInt(
                "primaryKitchenId",
                -1
            )
            val intent = Intent(this@MainActivity, KitchenListActivity::class.java)
            startActivity(intent)
        }

        val btnLogin = findViewById<Button>(R.id.btnLogin)
        val txtName = findViewById<EditText>(R.id.editTextUsername)
        val txtPassword = findViewById<EditText>(R.id.editTextPassword)
        val btnCreateAccount = findViewById<Button>(R.id.btnCreateAccount)

        btnLogin.setOnClickListener{
            // validate username and password

            if (txtName.text.isNullOrEmpty() || txtPassword.text.isNullOrEmpty())
                Toast.makeText(
                    applicationContext,
                    "Please enter your email and password",
                    Toast.LENGTH_LONG
                ).show()
            else
            {

                ServiceHandler.callAccountService(
                    "LoginAccount", hashMapOf(
                        "email" to txtName.text.toString(),
                        "pass" to txtPassword.text.toString()
                    ), this,
                    Response.Listener { response ->
                        val json = JSONObject(response.toString())
                        val user = json.getJSONObject("LoginAccountResult")
                        val userId = user.getInt("Id")

                        if (userId != -1) {
                            ServiceHandler.userId = userId
                            ServiceHandler.email = user.getString("Email")
                            ServiceHandler.vegan = user.getBoolean("Vegan")
                            ServiceHandler.vegetarian = user.getBoolean("Vegetarian")
                            var allergies = user.getJSONArray("Allergies")
                            ServiceHandler.allergies = ArrayList<String>()
                            for (i in 0 until allergies.length()) {
                                ServiceHandler.allergies!!.add(allergies.getString(i))
                            }
                            var kitchens = user.getJSONArray("Kitchens")
                            var kitchen = kitchens.getJSONObject(0)
                            ServiceHandler.primaryKitchenId = kitchen.getInt("Id")
                            //I really wish there was an easy way to serialize all of this
                            with(ServiceHandler.sharedPref.edit()) {
                                putInt("userId", ServiceHandler.userId)
                                putString("email", ServiceHandler.email)
                                putBoolean("vegan", ServiceHandler.vegan)
                                putBoolean("vegetarian", ServiceHandler.vegetarian)
                                putStringSet("allergies", ServiceHandler.allergies?.toHashSet())
                                putInt("primaryKitchenId", ServiceHandler.primaryKitchenId)
                                apply()
                            }
                            val intent = Intent(this@MainActivity, KitchenListActivity::class.java)
                            startActivity(intent)
                        } else {
                            Toast.makeText(
                                applicationContext,
                                "Email or password is incorrect",
                                Toast.LENGTH_LONG
                            ).show()
                        }

                    })


            }

        }

        btnCreateAccount.setOnClickListener{
            // go to create account activity
            val intent = Intent(this@MainActivity, CreateAccountActivity::class.java)
            startActivity(intent)
        }
    }

    fun myAlarm() {
        val calendar: Calendar = Calendar.getInstance()
        calendar.set(Calendar.HOUR_OF_DAY, 9)
        calendar.set(Calendar.MINUTE, 0)
        if (calendar.getTime().compareTo(Date()) < 0)
            calendar.add(Calendar.DAY_OF_MONTH, 1)
        val intent = Intent(applicationContext, NotificationReceiver::class.java)
        val pendingIntent = PendingIntent.getBroadcast(
            applicationContext,
            0,
            intent,
            PendingIntent.FLAG_UPDATE_CURRENT
        )
        val alarmManager = getSystemService(ALARM_SERVICE) as AlarmManager
        alarmManager?.setRepeating(
            AlarmManager.RTC_WAKEUP,
            calendar.getTimeInMillis(),
            AlarmManager.INTERVAL_DAY,
            pendingIntent
        )
    }

    /*
     * Override Android's default back button press
     */
    override fun onBackPressed() {
        // do nothing for now
    }
}

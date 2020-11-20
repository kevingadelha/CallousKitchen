package com.example.callouskitchenandroid

import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import com.android.volley.Response
import com.google.gson.Gson
import org.json.JSONObject

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        ServiceHandler.sharedPref = getPreferences(Context.MODE_PRIVATE)

        var userId = ServiceHandler.sharedPref.getInt("userId",-1)
        if (userId != -1){
            ServiceHandler.userId = userId
            ServiceHandler.email = ServiceHandler.sharedPref.getString("email",null)
            ServiceHandler.vegan = ServiceHandler.sharedPref.getBoolean("vegan",false)
            ServiceHandler.vegetarian = ServiceHandler.sharedPref.getBoolean("vegetarian",false)
            val allergies = ServiceHandler.sharedPref.getStringSet("allergies",null)?.toMutableList()
            if (allergies != null){
                ServiceHandler.allergies = ArrayList(allergies!!)
            }
            ServiceHandler.primaryKitchenId = ServiceHandler.sharedPref.getInt("primaryKitchenId",-1)
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
                Toast.makeText(applicationContext,"Please enter your email and password", Toast.LENGTH_LONG).show()
            else
            {

                ServiceHandler.callAccountService(
                    "LoginAccount",hashMapOf("email" to txtName.text.toString(),"pass" to txtPassword.text.toString()),this,
                    Response.Listener { response ->
                        val json = JSONObject(response.toString())
                        val user = json.getJSONObject("LoginAccountResult")
                        val userId = user.getInt("Id")

                        if (userId != -1){
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
                            with (ServiceHandler.sharedPref.edit()) {
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
                        }
                        else{
                            Toast.makeText(applicationContext,"Email or password is incorrect", Toast.LENGTH_LONG).show()
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
}

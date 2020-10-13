package com.example.callouskitchenandroid

import android.content.Intent
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

        val btnLogin = findViewById<Button>(R.id.btnLogin)
        val txtName = findViewById<EditText>(R.id.editTextUsername)
        val txtPassword = findViewById<EditText>(R.id.editTextPassword)
        val btnCreateAccount = findViewById<Button>(R.id.btnCreateAccount)

        btnLogin.setOnClickListener{
            // validate username and password

            if (txtName.text.isNullOrEmpty() || txtPassword.text.isNullOrEmpty())
                Toast.makeText(applicationContext,"Please enter your username and password", Toast.LENGTH_LONG).show()
            else
            {

                ServiceHandler.callAccountService(
                    "LoginAccount",hashMapOf("userName" to txtName.text.toString(),"pass" to txtPassword.text.toString()),this,
                    Response.Listener { response ->
                        val json = JSONObject(response.toString())
                        val user = json.getJSONObject("LoginAccountResult")
                        val userId = user.getInt("Id")

                        if (userId != -1){
                            ServiceHandler.userId = userId
                            ServiceHandler.email = user.getString("Email")
                            ServiceHandler.userName = user.getString("Username")
                            ServiceHandler.vegan = user.getBoolean("Vegan")
                            ServiceHandler.vegetarian = user.getBoolean("Vegetarian")
                            var allergies = user.getJSONArray("Allergies")
                            ServiceHandler.allergies = ArrayList<String>()
                            for (i in 0 until allergies.length()) {
                                ServiceHandler.allergies!!.add(allergies.getString(i))
                            }
                            val intent = Intent(this@MainActivity, KitchenListActivity::class.java)
                            startActivity(intent)
                        }
                        else{
                            Toast.makeText(applicationContext,"Username or password is incorrect", Toast.LENGTH_LONG).show()
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

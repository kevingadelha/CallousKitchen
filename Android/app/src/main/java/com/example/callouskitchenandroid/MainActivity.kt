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
                        val userId = json.getInt("LoginAccountResult")

                        if (userId != -1){
                            ServiceHandler.userId = userId
                            val intent = Intent(this@MainActivity, KitchenListActivity::class.java)
                            startActivity(intent)
                        }
                        else{
                            Toast.makeText(applicationContext,"Email or password is incorrect", Toast.LENGTH_LONG).show()
                        }

                    })


            }

        }
    }
}

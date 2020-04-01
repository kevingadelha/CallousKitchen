package com.example.callouskitchenandroid

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast

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
                Toast.makeText(applicationContext,"Please enter your username and password", Toast.LENGTH_LONG).show()
            else
            {
                val intent = Intent(this@MainActivity, KitchenListActivity::class.java)
                startActivity(intent)
            }

        }
    }
}

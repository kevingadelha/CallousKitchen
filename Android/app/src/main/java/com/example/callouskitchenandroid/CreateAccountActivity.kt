package com.example.callouskitchenandroid

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import com.android.volley.Response
import org.json.JSONObject

class CreateAccountActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_create_account)

        val btnCreate = findViewById<Button>(R.id.btnConfirmCreateAccount)
        val btnCancel = findViewById<Button>(R.id.btnCancelCreateAccount)
        val txtUsername = findViewById<EditText>(R.id.editTextUsernameCreate)
        val txtPassword = findViewById<EditText>(R.id.editTextCreatePassword)
        val txtConfirmPassword  = findViewById<EditText>(R.id.editTextConfirmPassword)

        btnCreate.setOnClickListener{
            // client side validation
            // all fields full?
            if (!(txtUsername.text.isNullOrEmpty() || txtPassword.text.isNullOrEmpty() || txtConfirmPassword.text.isNullOrEmpty()))
            {
                // passwords match?
                if (txtPassword.text.toString() == txtConfirmPassword.text.toString())
                {

                    val username = txtUsername.text.toString()
                    val password = txtPassword.text.toString()

                    ServiceHandler.callAccountService(
                        "CreateAccount",hashMapOf("userName" to username,"pass" to password),this,
                        Response.Listener { response ->
                            val json = JSONObject(response.toString())
                            val userId = json.getInt("CreateAccountResult")

                            if (userId != -1){
                                ServiceHandler.userId = userId
                                val intent = Intent(this@CreateAccountActivity, KitchenListActivity::class.java)
                                startActivity(intent)
                            }
                            else{
                                Toast.makeText(applicationContext,"Username exists", Toast.LENGTH_LONG).show()
                            }

                        })

                }
                else
                {
                    Toast.makeText(applicationContext,"Passwords don't match", Toast.LENGTH_LONG).show()
                }
            }
            else
            {
                Toast.makeText(applicationContext,"Please fill all fields", Toast.LENGTH_LONG).show()
            }
        }

        btnCancel.setOnClickListener{
            // return to login screen
            val intent = Intent(this@CreateAccountActivity, MainActivity::class.java)
            startActivity(intent)
        }

    }
}
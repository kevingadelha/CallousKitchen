package com.example.callouskitchenandroid

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.CheckBox
import android.widget.EditText
import android.widget.Toast
import com.android.volley.Response
import kotlinx.android.synthetic.main.activity_kitchen_list.*
import org.json.JSONObject

class SettingsActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_settings)

        // set up bottom nav bar
        setNavigation()

        val txtEmail = findViewById<EditText>(R.id.editSettingEmail)
        val btnSaveEmail = findViewById<Button>(R.id.btnSaveEmail)

        val txtPassword = findViewById<EditText>(R.id.editSettingPassword)
        val txtConfirmPassword = findViewById<EditText>(R.id.editSettingConfirmPassword)
        val btnResetPassword = findViewById<Button>(R.id.btnResetPassword)

        // dietary restriction and allergy checkboxes
        val chkBxVegetarian = findViewById<CheckBox>(R.id.chkBxVegetarian)
        val chkBxVegan = findViewById<CheckBox>(R.id.chkBxVegan)
        val chkBxPeanuts = findViewById<CheckBox>(R.id.chkBxPeanuts)
        val chkBxTreeNuts = findViewById<CheckBox>(R.id.chkBxTreeNuts)
        val chkBxDairy = findViewById<CheckBox>(R.id.chkBxDairy)
        val chkBxGluten = findViewById<CheckBox>(R.id.chkBxGluten)
        val chkBxShellfish = findViewById<CheckBox>(R.id.chkBxShellfish)
        val chkBxFish = findViewById<CheckBox>(R.id.chkBxFish)
        val chkBxEggs = findViewById<CheckBox>(R.id.chkBxEggs)
        val chkBxAllergy = findViewById<CheckBox>(R.id.chkBxAllergy)
        val txtAllergy = findViewById<EditText>(R.id.editAllergyText)
        val btnSaveDiet = findViewById<Button>(R.id.btnSaveDiet)

        // Disable the allergy field unless the box is checked
        txtAllergy.isEnabled = false

        //TODO: get the email and dietary restrictions for the user and update the fields accordingly



        btnSaveEmail.setOnClickListener {
            val email = txtEmail.text.toString()

            if (email.isNullOrEmpty())
            {
                Toast.makeText(applicationContext,"Please enter a valid email", Toast.LENGTH_LONG).show()
            }
            else
            {
                // TODO: update email
            }

        }

        btnResetPassword.setOnClickListener {
            if (!(txtPassword.text.isNullOrEmpty() || txtConfirmPassword.text.isNullOrEmpty()))
            {
                // passwords match?
                if (txtPassword.text.toString() == txtConfirmPassword.text.toString())
                {
                    val password = txtPassword.text.toString()

                    // TODO: update password

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

        btnSaveDiet.setOnClickListener {

            // TODO: decide how allergies/restrictions will be stored

        }

        chkBxAllergy.setOnClickListener {
            if (chkBxAllergy.isChecked)
                txtAllergy.isEnabled = true
            else
                txtAllergy.isEnabled = false
        }

    }

    private fun setNavigation() {
        bottomNav.setOnNavigationItemSelectedListener {
            when (it.itemId){
                R.id.navigation_recipes -> {
                    // go to recipes
                    val intent = Intent(this@SettingsActivity, RecipeSearchActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_inventory -> {
                    // go to the categories list
                    val intent = Intent(this@SettingsActivity, KitchenListActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.navigation_settings -> {
                    // go to settings (already here)
                    true
                }
                else -> false
            }
        }
    }
}
/* Authors: Kevin Gadelha, Laura Stewart
 *
 */
package com.example.callouskitchenandroid

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.*
import com.android.volley.Response
import kotlinx.android.synthetic.main.activity_kitchen_list.*
import org.json.JSONArray
import org.json.JSONObject

class SettingsActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_settings)

        // set up bottom nav bar
        setNavigation()

        // log the user out
        val btnLogout = findViewById<Button>(R.id.btnLogout)
        btnLogout.setOnClickListener {
            with (ServiceHandler.sharedPref.edit()) {
                putInt("userId", -1)
                putString("email", "")
                apply()
            }
            ServiceHandler.userId = -1

            val intent = Intent(this@SettingsActivity, MainActivity::class.java)
            startActivity(intent)
        }

        val txtEmail = findViewById<EditText>(R.id.editSettingEmail)
        val btnSaveEmail = findViewById<Button>(R.id.btnSaveEmail)

        val txtPassword = findViewById<EditText>(R.id.editSettingPassword)
        val txtConfirmPassword = findViewById<EditText>(R.id.editSettingConfirmPassword)
        val btnResetPassword = findViewById<Button>(R.id.btnResetPassword)

        // dietary restriction and allergy checkboxes
        val spinnerDiet = findViewById<Spinner>(R.id.spinnerDiet)
        val chkBxPeanuts = findViewById<CheckBox>(R.id.chkBxPeanuts)
        val chkBxTreeNuts = findViewById<CheckBox>(R.id.chkBxTreeNuts)
        val chkBxDairy = findViewById<CheckBox>(R.id.chkBxDairy)
        val chkBxGluten = findViewById<CheckBox>(R.id.chkBxGluten)
        val chkBxShellfish = findViewById<CheckBox>(R.id.chkBxShellfish)
        val chkBxFish = findViewById<CheckBox>(R.id.chkBxFish)
        val chkBxEggs = findViewById<CheckBox>(R.id.chkBxEggs)
        val chkBxSoy = findViewById<CheckBox>(R.id.chkBxSoy)
        val chkBxAllergy = findViewById<CheckBox>(R.id.chkBxAllergy)
        val txtAllergy = findViewById<EditText>(R.id.editAllergyText)
        val btnSaveDiet = findViewById<Button>(R.id.btnSaveDiet)
        val btnCancel = findViewById<Button>(R.id.btnCancelEditDiet)

        val checkBoxes = listOf(chkBxPeanuts, chkBxTreeNuts, chkBxDairy, chkBxGluten, chkBxShellfish, chkBxFish, chkBxSoy, chkBxEggs)

        // populate the diet spinner
        val diets = listOf("Omnivore", "Vegetarian", "Vegan")
        val adapter = ArrayAdapter(this, R.layout.custom_spinner_item, diets)
        spinnerDiet.adapter = adapter

        // Disable the allergy field unless the box is checked
        txtAllergy.isEnabled = false

        // Set the spinner of dietary restrictions to the correct value
        when {
            ServiceHandler.vegan -> {
                spinnerDiet.setSelection(diets.indexOf("Vegan"))
            }
            ServiceHandler.vegetarian -> {
                spinnerDiet.setSelection(diets.indexOf("Vegetarian"))
            }
            else -> {
                spinnerDiet.setSelection(diets.indexOf("Omnivore"))
            }
        }

        val allergies = ServiceHandler.allergies
        checkBoxes.forEach(){
            if (allergies?.contains(it.text.toString().toLowerCase()) ?: false){
                it.isChecked = true
                allergies!!.remove(it.text.toString().toLowerCase())
            }
        }

        if (allergies?.size!! > 0){
            txtAllergy.setText(allergies!![0])
            chkBxAllergy.isChecked = true
            txtAllergy.isEnabled = true
        }

        btnSaveEmail.setOnClickListener {
            val email = txtEmail.text.toString()

            if (email.isNullOrEmpty())
            {
                Toast.makeText(applicationContext,"Please enter a valid email", Toast.LENGTH_LONG).show()
            }
            else
            {
                ServiceHandler.callAccountService(
                    "EditUserEmail", hashMapOf(
                        "id" to ServiceHandler.userId,
                        "email" to email
                    ), this,
                    Response.Listener { response ->
                        val json = JSONObject(response.toString())
                        val success = json.getBoolean("EditUserEmailResult")
                        if (success){
                            ServiceHandler.email = email
                            with (ServiceHandler.sharedPref.edit()) {
                                putInt("userId", ServiceHandler.userId)
                                apply()
                            }
                            Toast.makeText(applicationContext,"Saved :)", Toast.LENGTH_LONG).show()
                        }
                        else{
                            Toast.makeText(applicationContext,"Failed :(", Toast.LENGTH_LONG).show()
                        }

                    })
            }

        }

        btnResetPassword.setOnClickListener {
            if (!(txtPassword.text.isNullOrEmpty() || txtConfirmPassword.text.isNullOrEmpty()))
            {
                // passwords match?
                if (txtPassword.text.toString() == txtConfirmPassword.text.toString())
                {
                    val password = txtPassword.text.toString()


                    ServiceHandler.callAccountService(
                        "EditUserPassword", hashMapOf(
                            "id" to ServiceHandler.userId,
                            "password" to password
                        ), this,
                        Response.Listener { response ->
                            val json = JSONObject(response.toString())
                            val success = json.getBoolean("EditUserPasswordResult")
                            if (success){
                                Toast.makeText(applicationContext,"Saved :)", Toast.LENGTH_LONG).show()
                            }
                            else{
                                Toast.makeText(applicationContext,"Failed :(", Toast.LENGTH_LONG).show()
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

        btnSaveDiet.setOnClickListener {

            var allergies = ArrayList<String>()
            checkBoxes.forEach(){
                if (it.isChecked){
                    allergies.add(it.text.toString().toLowerCase())
                }
            }
            if (chkBxAllergy.isChecked){
                if (txtAllergy.text.toString().isNullOrEmpty()){
                    Toast.makeText(applicationContext,"Type in your allergy", Toast.LENGTH_LONG).show()
                    return@setOnClickListener
                }
                else{
                    allergies.add(txtAllergy.text.toString().toLowerCase())
                }
            }

            var vegan = false
            var vegetarian = false

            if (spinnerDiet.selectedItem == "Vegan") {
                vegan = true
            }
            else if (spinnerDiet.selectedItem == "Vegetarian") {
                vegetarian = true
            }

            ServiceHandler.callAccountService(
                "EditUserDietaryRestrictions", hashMapOf(
                    "id" to ServiceHandler.userId,
                    "vegan" to vegan,
                    "vegetarian" to vegetarian,
                    "allergies" to JSONArray(allergies)
                ), this,
                Response.Listener { response ->
                    val json = JSONObject(response.toString())
                    val success = json.getBoolean("EditUserDietaryRestrictionsResult")
                    if (success){
                        Toast.makeText(applicationContext,"Saved :)", Toast.LENGTH_LONG).show()
                        ServiceHandler.vegan = vegan
                        ServiceHandler.vegetarian = vegetarian
                        ServiceHandler.allergies = allergies
                        with (ServiceHandler.sharedPref.edit()) {
                            putBoolean("vegan", ServiceHandler.vegan)
                            putBoolean("vegetarian", ServiceHandler.vegetarian)
                            putStringSet("allergies", ServiceHandler.allergies?.toHashSet())
                            apply()
                        }
                    }
                    else{
                        Toast.makeText(applicationContext,"Failed :(", Toast.LENGTH_LONG).show()
                    }

                })


        }

        chkBxAllergy.setOnClickListener {
            txtAllergy.isEnabled = chkBxAllergy.isChecked
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
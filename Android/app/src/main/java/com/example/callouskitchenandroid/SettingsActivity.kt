package com.example.callouskitchenandroid

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.CheckBox
import android.widget.EditText

class SettingsActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_settings)

        val txtEmail = findViewById<EditText>(R.id.editSettingEmail)
        val txtPassword = findViewById<EditText>(R.id.editSettingPassword)
        val btnSaveAccount = findViewById<Button>(R.id.btnSaveAccount)

        val chkBxVegetarian = findViewById<CheckBox>(R.id.chkBxVegetarian)
        val chkBxVegan = findViewById<CheckBox>(R.id.chkBxVegan)
        val chkBxAllergy = findViewById<CheckBox>(R.id.chkBxAllergy)
        val txtAllergy = findViewById<EditText>(R.id.editAllergyText)
        val btnSaveDiet = findViewById<Button>(R.id.btnSaveDiet)

        txtAllergy.isEnabled = false

        btnSaveAccount.setOnClickListener {

        }

        chkBxAllergy.setOnClickListener {
            if (chkBxAllergy.isChecked)
                txtAllergy.isEnabled = true
            else
                txtAllergy.isEnabled = false

        }

    }
}
package com.example.callouskitchenandroid

import android.icu.text.BidiClassifier
import java.io.Serializable
import java.time.LocalDate

class Food: Serializable {

    var id: Int = 0;
    var name: String = ""
    var barcode: Int = 0
    var expiryDate: LocalDate? = LocalDate.parse("2020-01-01")
    var quantity: Double = 0.0
    var quantityClassifier: String = ""
    var favourite: Boolean = false

    constructor(id: Int, name: String, quantity: Double = 1.0, barcode: Int = 0, quantityClassifier: String = "")
    {
        this.id = id
        this.name = name
        this.quantity = quantity
        this.quantityClassifier = quantityClassifier
        this.barcode = barcode
    }

    constructor(id: Int, name: String, quantity: Double = 1.0, barcode: Int = 0, expiry: LocalDate, quantityClassifier: String = "")
    {
        this.id = id
        this.name = name
        this.quantity = quantity
        this.quantityClassifier = quantityClassifier
        this.barcode = barcode
        this.expiryDate = expiry
    }
}
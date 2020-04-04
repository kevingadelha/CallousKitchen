package com.example.callouskitchenandroid

import java.io.Serializable
import java.time.LocalDate

class Food: Serializable {

    var id: Int = 0;
    var name: String = ""
    var quantity: Int = 0;
    var barcode: Int = 0
    var expiryDate: LocalDate = LocalDate.parse("2020-01-01")

    constructor(id: Int, name: String, quantity: Int = 1, barcode: Int = 0)
    {
        this.id = id
        this.name = name
        this.quantity = quantity
        this.barcode = barcode
    }

    constructor(id: Int, name: String, quantity: Int = 1, barcode: Int = 0, expiry: LocalDate)
    {
        this.id = id
        this.name = name
        this.quantity = quantity
        this.barcode = barcode
        this.expiryDate = expiry
    }
}
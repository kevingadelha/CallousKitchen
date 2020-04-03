package com.example.callouskitchenandroid

import java.io.Serializable
import java.time.LocalDate

class Food: Serializable {

    var id: Int = 0;
    var name: String = ""
    var barcode: Int = 0
    var expiryDate: LocalDate = LocalDate.parse("2020-01-01")
    var quantity: Double = 0.0

    constructor(id: Int, name: String)
    {
        this.id = id
        this.name = name
    }

    constructor(id: Int, name: String, barcode: Int)
    {
        this.id = id
        this.name = name
        this.barcode = barcode
    }

    constructor(id: Int, name: String, barcode: Int, expiry: LocalDate)
    {
        this.id = id
        this.name = name
        this.barcode = barcode
        this.expiryDate = expiry
    }
}
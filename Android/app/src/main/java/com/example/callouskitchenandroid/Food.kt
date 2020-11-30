/* Authors: Kevin Gadelha, Laura Stewart */
package com.example.callouskitchenandroid

import java.io.Serializable
import java.time.LocalDate

class Food: Serializable {

    var id: Int = 0;
    var name: String = ""
    var barcode: Int = 0
    var expiryDate: LocalDate? = LocalDate.parse("2020-01-01")
    var quantity: Double = 0.0
    var quantityClassifier: String = ""
    var storage: String = ""
    var favourite: Boolean = false
    var onShoppingList: Boolean = false
    var vegan: Int? = -1
    var vegetarian: Int? = -1
    var ingredients = arrayOf<String>()
    var traces = arrayOf<String>()

    constructor(id: Int, name: String, quantity: Double = 1.0, barcode: Int = 0, quantityClassifier: String = "")
    {
        this.id = id
        this.name = name
        this.quantity = quantity
        this.quantityClassifier = quantityClassifier
        this.barcode = barcode
    }

    constructor(ingredients : Array<String>,traces : Array<String>, vegan : Int?, vegetarian : Int?)
    {
        this.ingredients = ingredients
        this.traces = traces
        this.vegan = vegan
        this.vegetarian = vegetarian
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
package com.example.callouskitchenandroid

class Recipe {
    var name: String = ""
    var url: String = ""
    var source: String = ""
    var image: String = ""
    var recipeYield: Double = 0.0

    constructor(name: String, url: String, source: String, image: String, recipeYield: Double)
    {
        this.name = name
        this.url = url
        this.source = source
        this.image = image
        this.recipeYield = recipeYield
    }
}
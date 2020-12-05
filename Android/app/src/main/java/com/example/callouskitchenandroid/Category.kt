/* Authors: Kevin Gadelha, Laura Stewart */
package com.example.callouskitchenandroid

class Category
{
    var id: Int = 0
    var name: String = ""
    var foods: ArrayList<Food> = arrayListOf<Food>()

    constructor(id:Int, name:String)
    {
        this.id = id
        this.name = name
    }

    constructor(id:Int, name:String, foods: ArrayList<Food>)
    {
        this.id = id
        this.name = name
        this.foods = foods
    }
}
/* Authors: Kevin Gadelha, Laura Stewart */
package com.example.callouskitchenandroid

class Kitchen
{
    // will add more to this later, just getting UI working for now
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
package com.example.callouskitchenandroid

class Category
{

    // will add more to this later, just getting UI working for now
    var id: Int = 0
    var name: String = ""
    var foods: ArrayList<Food> = arrayListOf<Food>()
    var kitchenId: Int = 0

    constructor(id:Int, name:String, kitchenId:Int)
    {
        this.id = id
        this.name = name
        this.kitchenId = kitchenId
    }

    constructor(id:Int, name:String, foods: ArrayList<Food>, kitchenId: Int)
    {
        this.id = id
        this.name = name
        this.foods = foods
        this.kitchenId = kitchenId
    }
}
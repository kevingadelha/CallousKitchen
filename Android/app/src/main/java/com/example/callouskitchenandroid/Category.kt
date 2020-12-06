/* Authors: Laura Stewart */
package com.example.callouskitchenandroid

/**
 * Represents a category in the kitchen
 *
 * @author Laura Stewart
 */
class Category
{
    var id: Int = 0
    var name: String = ""
    var foods: ArrayList<Food> = arrayListOf<Food>()

    /**
     * Creates a Category with an Id and name
     *
     * @param id The category's index
     * @param name The category title
     * @author Laura Stewart
     */
    constructor(id:Int, name:String)
    {
        this.id = id
        this.name = name
    }

    /**
     * Creates a Category with an Id, name, and list of foods in it (never used)
     *
     * @param id The category's index
     * @param name The category title
     * @param foods All food in this category
     * @author Laura Stewart
     */
    constructor(id:Int, name:String, foods: ArrayList<Food>)
    {
        this.id = id
        this.name = name
        this.foods = foods
    }
}
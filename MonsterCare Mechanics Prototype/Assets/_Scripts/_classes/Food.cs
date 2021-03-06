using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : InventoryItem
{
    string foodType;
    int foodPower;
    string element;
    int SpriteIndex;

    /// <summary>
    /// Create a normal/empty Food item.
    /// </summary>
    public Food(bool empty)
    {
        if (!empty) 
        {
            path = "Prefabs/FeedingPrototype/FOOD";
            foodType = "Normal";
            Type = "Food";
            element = "None";
            foodPower = 20;
            SpriteIndex = -1;
        }
        else 
        {
            path = "None";
            foodType = "None";
            Type = "Food";
            element = "None";
            foodPower = 0;
            SpriteIndex = -1;
        }
    }


    /// <summary>
    /// Create a special food item with an element type.
    /// </summary>
    /// <param name="elementType">Set the element type: Earth, Ice, Fire, or Wind</param>
    public Food(string elementType) 
    {
        path = "Prefabs/FeedingPrototype/FOOD";
        foodType = "Special";
        Type = "Food";
        element = elementType;
        foodPower = 30;
    }


    /// <summary>
    /// Create a special food item with an element type and custom food power.
    /// </summary>
    /// <param name="elementType">Set the element type: Earth, Ice, Fire, or Wind</param>
    /// <param name="power">Set the food power</param>
    public Food(string elementType, int power)
    {
        path = "Prefabs/FeedingPrototype/FOOD";
        foodType = "Special";
        Type = "Food";
        element = elementType;
        foodPower = power;
    }


    /// <summary>
    /// Create a special food item with an element type and custom food power.
    /// </summary>
    /// <param name="elementType">Set the element type: Earth, Ice, Fire, or Wind</param>
    /// <param name="power">Set the food power</param>
    /// <param name="FoodType">Set the type of food. Normal or Special</param>
    public Food(string FoodType, string elementType, int power)
    {
        path = "Prefabs/FeedingPrototype/FOOD";
        foodType = FoodType;
        element = elementType;
        foodPower = power;
    }

    public string FoodType 
    {
        get => foodType;
        set => foodType = value;
    }

    public int Power 
    {
        get => foodPower;
        set => foodPower = value;
    }

    public string Element 
    {
        get => element;
        set => element = value;
    }

    public int Sprite 
    {
        get => SpriteIndex;
        set => SpriteIndex = value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : InventoryItem
{
    string foodType;
    int foodPower;
    string element;


    /// <summary>
    /// Create a new Food item
    /// </summary>
    /// <param name="type">Set the food type, Special or Normal</param>
    public Food(string type) 
    {
        foodType = type;
        SetItemType = "Food";
        slot = -1;
        foodPower = 10;
    }


    /// <summary>
    /// Create a new Food item
    /// </summary>
    /// <param name="type">Set the food type, special or normal</param>
    /// <param name="elementType">Set the element type: Earth, Ice, Fire, or Wind</param>
    public Food(string type, string elementType) 
    {
        foodType = type;
        SetItemType = "Food";
        element = elementType;
        slot = -1;
        foodPower = 10;
    }


    /// <summary>
    /// Create a new Food item
    /// </summary>
    /// <param name="type">Set the food type, Special or Normal</param>
    /// <param name="power">Set the food power</param>
    public Food(string type, int power)
    {
        foodType = type;
        SetItemType = "Food";
        slot = -1;
        foodPower = power;
    }


    /// <summary>
    /// Create a new Food item
    /// </summary>
    /// <param name="type">Set the food type, special or normal</param>
    /// <param name="elementType">Set the element type: Earth, Ice, Fire, or Wind</param>
    /// <param name="power">Set the food power</param>
    public Food(string type, string elementType, int power)
    {
        foodType = type;
        SetItemType = "Food";
        element = elementType;
        slot = -1;
        foodPower = power;
    }
}

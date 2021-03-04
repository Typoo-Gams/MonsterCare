using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    //Item variables
    string ItemType;
    int InventorySlot;
    string prefabPath;


    /// <summary>
    /// Set/Get the item prefab path
    /// </summary>
    public string path 
    {
        get => prefabPath;
        set => prefabPath = value;
    }


    /// <summary>
    /// Get/Set item type
    /// </summary>
    public string Type
    {
        get => ItemType;
        set => ItemType = value;
    }


    /// <summary>
    /// Get/Set iventory slot index
    /// </summary>
    public int slot 
    {
        get => InventorySlot;
        set => InventorySlot = value;
    }
}

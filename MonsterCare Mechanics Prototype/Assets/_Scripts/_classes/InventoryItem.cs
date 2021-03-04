using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    //Item variables
    string ItemType;
    int InventorySlot;

    public InventoryItem() 
    {
        ItemType = "none";
        InventorySlot = -1;
    }

    public InventoryItem(string type) 
    {
        ItemType = type;
        InventorySlot = -1;
    }

    public InventoryItem(string type, int slot)
    {
        ItemType = type;
        InventorySlot = slot;
    }


    /// <summary>
    /// Get/Set item type
    /// </summary>
    public string SetItemType
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

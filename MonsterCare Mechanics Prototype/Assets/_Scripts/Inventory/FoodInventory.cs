using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodInventory : MonoBehaviour
{
    GameManager manager;
    GameSaver saver = new GameSaver();
    public int inventorySlot;

    public GameObject FoodPrefab;



    GameObject Spawned;
    FoodManager_FoodObject spawn;

    private void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();

        //Spawns a food at this Inventory Anchor.
        if (manager.FoodInventory[inventorySlot] != null)
        {
            //Sets the food values for the spawned food.
            if (manager.FoodInventory[inventorySlot].FoodType != "None")
            {
                Spawned = Instantiate(FoodPrefab);
                Spawned.transform.SetParent(transform, false);
                spawn = Spawned.GetComponent<FoodManager_FoodObject>();
                spawn.SpriteIndex = manager.FoodInventory[inventorySlot].Sprite;
                spawn.inventorySpace = inventorySlot;
                spawn.FoodCategory = manager.FoodInventory[inventorySlot].FoodType;
                spawn.FoodElement = manager.FoodInventory[inventorySlot].Element;
                spawn.FoodPower = manager.FoodInventory[inventorySlot].Power;
                GetComponent<LockOnAnchor>().LockedObject = Spawned;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheat_DeleteFood_Button : MonoBehaviour
{
    GameManager manager;
    GameSaver Save = new GameSaver();
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(TaskOnClick);
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
    }

    private void TaskOnClick()
    {
        manager.FoodInventory[0] = new Food();
        manager.FoodInventory[1] = new Food();
        manager.FoodInventory[2] = new Food();
        manager.FoodInventory[3] = new Food();
        manager.FoodInventory[4] = new Food();
        Save.SaveFood(manager.FoodInventory);
    }
}

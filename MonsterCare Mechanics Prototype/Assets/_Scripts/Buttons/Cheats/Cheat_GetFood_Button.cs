using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheat_GetFood_Button : MonoBehaviour
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
        manager.FoodInventory[0] = new Food(Random.Range(0, 12));
        manager.FoodInventory[1] = new Food(MonsterElement.Water);
        manager.FoodInventory[2] = new Food(MonsterElement.Fire);
        manager.FoodInventory[3] = new Food(MonsterElement.Air);
        manager.FoodInventory[4] = new Food(MonsterElement.Earth);
        Save.SaveFood(manager.FoodInventory);
    }
}

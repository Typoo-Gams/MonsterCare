using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodInventory : MonoBehaviour
{
    GameManager manager;
    GameSaver saver = new GameSaver();
    Button ThisButton;
    public int inventorySlot;

    public GameObject FoodPrefab;
    public GameObject parent;

    /// <summary>
    /// Index: 0 - Air, 1 - Earth, 2 - Fire, 3 - Water
    /// </summary>
    public Sprite[] Elemental = new Sprite[4];
    public Sprite Normal;

    FoodManager_FoodObject spawn;

    private void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        ThisButton = gameObject.GetComponent<Button>();
        ThisButton.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn == null) 
        {
            //changes the sprite if there is a food item in the inventory
            if (manager.FoodInventory[inventorySlot].FoodType != "None")
            {
                ThisButton.interactable = true;
                ThisButton.image.color = new Color(1, 1, 1, 1f);

                if (manager.FoodInventory[inventorySlot].FoodType == "Normal")
                {
                    ThisButton.image.sprite = Normal;
                }
                else
                {
                    switch (manager.FoodInventory[inventorySlot].Element)
                    {
                        case "Air":
                            ThisButton.image.sprite = Elemental[0];
                            break;
                        case "Earth":
                            ThisButton.image.sprite = Elemental[1];
                            break;
                        case "Fire":
                            ThisButton.image.sprite = Elemental[2];
                            break;
                        case "Water":
                            ThisButton.image.sprite = Elemental[3];
                            break;
                    }
                }
            }
            else
            {
                ThisButton.image.sprite = null;
                ThisButton.interactable = false;
                ThisButton.image.color = new Color(0, 0, 0, 0.2705882f);
            }
        }
    }


    // TaskOnClick is called when this button is pressed.s
    void TaskOnClick() 
    {
        spawn = Instantiate(FoodPrefab, parent.transform).GetComponent<FoodManager_FoodObject>();
        spawn.inventorySpace = inventorySlot;
        spawn.FoodCategory = manager.FoodInventory[inventorySlot].FoodType;
        spawn.FoodCategory = manager.FoodInventory[inventorySlot].FoodType;
        spawn.FoodPower = manager.FoodInventory[inventorySlot].Power;
        ThisButton.interactable = false;
    }
}

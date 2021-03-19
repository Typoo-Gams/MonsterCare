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
    public GameObject SpawnLocation;

    /// <summary>
    /// Index: 0 - Air, 1 - Earth, 2 - Fire, 3 - Water
    /// </summary>
    public Sprite[] Elemental = new Sprite[4];
    public Sprite[] Normal;

    GameObject Spawned;
    FoodManager_FoodObject spawn;

    bool IsActive;

    private void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        ThisButton = gameObject.GetComponent<Button>();
        ThisButton.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.FoodInventory[inventorySlot] != null) 
        {
            //changes the sprite if there is a food item in the inventory
            if (manager.FoodInventory[inventorySlot].FoodType != "None")
            {
                IsActive = true;
                if (GameObject.FindGameObjectWithTag("Food") == null)
                    ThisButton.interactable = IsActive;
                ThisButton.image.color = new Color(1, 1, 1, 1f);

                if (manager.FoodInventory[inventorySlot].FoodType == "Normal")
                {
                    if (manager.FoodInventory[inventorySlot].Sprite <= Normal.Length - 1)
                        ThisButton.image.sprite = Normal[manager.FoodInventory[inventorySlot].Sprite];
                    else
                        Debug.Log(Normal.Length);
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
                IsActive = false;
                ThisButton.interactable = IsActive;
                ThisButton.image.color = new Color(0, 0, 0, 0.2705882f);
            }
        }
    }


    // TaskOnClick is called when this button is pressed.sz
    void TaskOnClick() 
    {
        GameObject[] FoodButtons = GameObject.FindGameObjectsWithTag("FoodButton");

        if (GameObject.FindGameObjectWithTag("Food") == null)
        {
            Spawned = Instantiate(FoodPrefab);
            Spawned.transform.SetParent(parent.transform, false);
            Debug.Log(SpawnLocation.transform.position);
            Spawned.transform.position = SpawnLocation.transform.position;
            Spawned.transform.localScale = new Vector3(100, 100, 100);
            spawn = Spawned.GetComponent<FoodManager_FoodObject>();
            spawn.SpriteIndex = manager.FoodInventory[inventorySlot].Sprite;
            spawn.inventorySpace = inventorySlot;
            spawn.FoodCategory = manager.FoodInventory[inventorySlot].FoodType;
            spawn.FoodElement = manager.FoodInventory[inventorySlot].Element;
            spawn.FoodPower = manager.FoodInventory[inventorySlot].Power;

            foreach(GameObject button in FoodButtons) 
            {
                if (button.name != gameObject.name)
                {
                    IsActive = false;
                    button.GetComponent<Button>().interactable = IsActive;
                }
            }
        }
        else
            Destroy(Spawned);
    }
}

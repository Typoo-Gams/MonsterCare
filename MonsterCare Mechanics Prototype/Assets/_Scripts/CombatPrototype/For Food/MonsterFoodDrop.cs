using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MonsterFoodDrop : MonoBehaviour
{
    GameManager manager;

    public GameObject[] foodPrefab;
    public GameObject[] specialPrefab;
    public Sprite[] specialSprites;

    public bool isCreated;
    bool slideBar = true;
    int element;
    string spawnElement;

    // Start is called before the first frame update
    void Start()
    {
        string scene = SceneManager.GetActiveScene().name;
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        isCreated = false;

        switch (scene)
        {
            case "Savannah_FS":
                element = 0;
                spawnElement = "Fire";
                break;

            case "Desert_FS":
                element = 0;
                spawnElement = "Fire";
                break;

            case "Ice_FS":
                element = 1;
                spawnElement = "Water";
                break;

            case "Mountain_FS":
                element = 2;
                spawnElement = "Earth";
                break;

            case "Forest_FS":
                element = 3;
                spawnElement = "Air";
                break;
        }
    }

    private void Update()
    { 
        if(manager.EnemyMonster != null)
        {
            if (manager.EnemyMonster.DeathStatus)
            {
                if (!manager.EnemyMonster.GetHealthbar().IsActive())
                {
                    slideBar = false;
                    Debug.Log("sliders?");
                }
            }
        }
        
        
        if (!slideBar && isCreated == false)
        {
            isCreated = true;
            FoodDrop();
        }
    }

    private void FoodDrop()
    {
        int random = Random.Range(0, foodPrefab.Length);
        int dropRate = Random.Range(1, 11);

        if (isCreated == true)
        {
            Debug.LogWarning(manager.FoodInventory.Length);
            for(int i = 0; i < manager.FoodInventory.Length; i++)
            {
                Debug.LogWarning("searching inv");
                if(manager.FoodInventory[i] == null)
                {
                    if(dropRate == 1)
                    {
                        //GameObject spawn = Instantiate(specialPrefab[element]);
                        manager.FoodInventory[i] = new Food(spawnElement);
                        manager.FoodReward = manager.FoodInventory[i];
                    }
                    else 
                    {
                        GameObject spawn = Instantiate(foodPrefab[random]);
                        manager.FoodInventory[i] = new Food(false);
                        manager.FoodReward = manager.FoodInventory[i];
                    }
                    Destroy(manager.Enemy.gameObject);
                    Destroy(GameObject.FindGameObjectWithTag("CanvasFighting"));
                    manager.EnemyMonster = null;
                    Debug.Log("Destroyed");
                    break;
                }
                else
                {
                    Debug.Log("Inventory slot " + i + " is full");
                }   
            }
        }
    }
}

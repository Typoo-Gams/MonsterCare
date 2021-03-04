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

    public bool isCreated;
    bool slideBar = true;
    int element;

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
                break;

            case "Desert_FS":
                element = 0;
                break;

            case "Ice_FS":
                element = 1;
                break;

            case "Mountain_FS":
                element = 2;
                break;

            case "Forest_FS":
                element = 3;
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
                        manager.FoodInventory[i] = Instantiate(specialPrefab[element]);
                    }
                    manager.FoodInventory[i] = Instantiate(foodPrefab[random]);
                    Destroy(manager.Enemy.gameObject);
                    Destroy(GameObject.FindGameObjectWithTag("CanvasFighting"));
                    manager.EnemyMonster = null;
                    Debug.Log("Destroyed");
                    break;
                }
                else
                {
                    Debug.Log("Inventory is full");
                }   
            }
            
        }
    }

    
}

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
    public GameObject FullInventory;
    public Sprite[] specialSprites;

    public bool isCreated;
    bool slideBar = true;
    int element;
    MonsterElement spawnElement;

    char[] seperator = { '_' };
    int specialFoodChance;

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
                spawnElement = MonsterElement.Fire;
                break;

            case "Desert_FS":
                element = 0;
                spawnElement = MonsterElement.Fire;
                break;

            case "Ice_FS":
                element = 1;
                spawnElement = MonsterElement.Water;
                break;

            case "Mountain_FS":
                element = 2;
                spawnElement = MonsterElement.Earth;
                break;

            case "Forest_FS":
                element = 3;
                spawnElement = MonsterElement.Air;
                break;
        }

        switch (manager.MonsterObject.name.Split(seperator)[1])
        {
            case "Gen0":
                specialFoodChance = 2;
                break;

            case "Gen1":
                specialFoodChance = 1;
                break;
        }
    }

    private void Update()
    { 
        if(manager.EnemyMonster != null)
        {
            if (manager.EnemyMonster.DeathStatus)
            {
                if (!manager.EnemyMonster.GetHealthbar().activeSelf)
                {
                    slideBar = false;
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
        int dropRate = Random.Range(1, 7);

        if (isCreated == true)
        {
            Debug.LogWarning(manager.FoodInventory.Length);
            for(int i = 0; i < manager.FoodInventory.Length; i++)
            {
                Debug.LogWarning("searching inv");
                if(manager.FoodInventory[i].FoodType == "None")
                {
                    if(dropRate == specialFoodChance)
                    {
                        GameObject spawn = Instantiate(specialPrefab[element]);
                        spawn.transform.SetParent(GameObject.FindGameObjectWithTag("CanvasFighting").transform, false);
                        manager.FoodInventory[i] = new Food(spawnElement);
                        manager.FoodReward = manager.FoodInventory[i];
                        Debug.Log("Special food");
                    }
                    else 
                    {
                        GameObject spawn = Instantiate(foodPrefab[random]);
                        spawn.transform.SetParent(GameObject.FindGameObjectWithTag("CanvasFighting").transform, false);
                        manager.FoodInventory[i] = new Food(false);
                        manager.FoodReward = manager.FoodInventory[i];
                        Debug.Log("Normal food");
                    }
                    Destroy(manager.Enemy.gameObject);
                    Destroy(manager.ActiveMonster.GetHealthbar());
                    manager.EnemyMonster = null;
                    Debug.Log("Enemey destroyed");
                    break;
                }
                else
                {
                    Debug.Log("Inventory slot " + i + " is full");
                    if (i == manager.FoodInventory.Length - 1) 
                    {
                        Debug.LogWarning("The inventory is full");

                        GameObject spawn = Instantiate(FullInventory);
                        spawn.transform.SetParent(GameObject.FindGameObjectWithTag("CanvasFighting").transform, false);
                        Destroy(manager.Enemy.gameObject);
                        Destroy(manager.ActiveMonster.GetHealthbar());
                        manager.EnemyMonster = null;
                        Debug.Log("Enemey destroyed");
                        break;
                    }
                }   
            }
        }
    }
}

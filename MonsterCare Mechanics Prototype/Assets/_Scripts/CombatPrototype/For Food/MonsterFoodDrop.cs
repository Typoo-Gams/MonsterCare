using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterFoodDrop : MonoBehaviour
{
    GameManager manager;

    public GameObject[] foodPrefab;
    public bool isCreated;
    bool slideBar = true;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        isCreated = false;
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

        if (isCreated == true)
        {
            Debug.LogWarning(manager.FoodInventory.Length);
            for(int i = 0; i < manager.FoodInventory.Length; i++)
            {
                Debug.LogWarning("searching inv");
                if(manager.FoodInventory[i] == null)
                {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterFoodDrop : MonoBehaviour
{

    public MonsterManager_AttackPrototype KillThisMonster;
    public GameObject foodPrefab;
    float currentHealth;
    public bool isCreated;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = KillThisMonster.StartMonster.HealthStatus;
        isCreated = false;
    }

    private void Update()
    {
        if(KillThisMonster.StartMonster.DeathStatus && isCreated == false)
        {
            isCreated = true;
            FoodDrop();
        }
    }

    private void FoodDrop()
    {
        if (isCreated == true)
        {
            Instantiate(foodPrefab);
            Destroy(KillThisMonster.gameObject);
        }
    }

    
}

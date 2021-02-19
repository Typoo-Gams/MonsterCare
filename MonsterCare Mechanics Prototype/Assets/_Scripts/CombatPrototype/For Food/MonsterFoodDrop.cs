using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterFoodDrop : MonoBehaviour
{

    public MonsterManager_AttackPrototype KillThisMonster;
    public GameObject foodPrefab;
    public bool isCreated;
    bool slideBar = true;

    // Start is called before the first frame update
    void Start()
    {
        isCreated = false;
    }

    private void Update()
    {
        if (KillThisMonster != null)
            if (KillThisMonster.StartMonster.DeathStatus) 
                slideBar = KillThisMonster.StartMonster.GetHealthbar().IsActive();
        
        if (!slideBar && isCreated == false)
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
            KillThisMonster = null;
        }
    }

    
}

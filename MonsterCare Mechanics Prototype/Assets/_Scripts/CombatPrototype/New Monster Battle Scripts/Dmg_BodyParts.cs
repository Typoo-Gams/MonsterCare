using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dmg_BodyParts : MonoBehaviour
{
    public MonsterManager_AttackPrototype HealthStats;
    float currentHealth;
    public List<GameObject> colliders = new List<GameObject>();

    float Head = 2f;

    private void Start()
    {
        currentHealth = HealthStats.StartMonster.HealthStatus;
    }

    private void Update()
    {
        Attacking();
    }

    private void Attacking()
    {
        if(Input.touchCount > 0)
        {
            if(gameObject.tag == "Head")
            {
                HealthStats.StartMonster.UpdateHealth(currentHealth - Head);
            }
        }
        
    }
}

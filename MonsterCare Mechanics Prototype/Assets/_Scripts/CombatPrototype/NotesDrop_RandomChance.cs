using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesDrop_RandomChance : MonoBehaviour
{
    public MonsterManager_AttackPrototype IsItDead;
    float currentHealth;
    const float dropChance = 1f / 10f;
    public string[] notes = new string[4];

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = IsItDead.StartMonster.HealthStatus;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyHasDied()
    {
        if(Random.Range(0f, 1f) <= dropChance)
        {

        }
    }
}

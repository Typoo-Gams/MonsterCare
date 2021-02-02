using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MikkelTestMonster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Monster monster = new Monster("Mikkel");

        monster.HealthStatus = 100;
        monster.SleepStatus = 10;
        if (monster.CombatStatus)
        {

        }
        //monster.CombatActive(true);          

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

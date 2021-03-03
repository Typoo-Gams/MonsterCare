using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dmg_BodyParts : MonoBehaviour
{
    GameManager manager;

    bool currentlyAttacking;
    public int DmgModifier;
    float CounterShake;
    float intervalShake;
    bool HasMoved = false;

    private void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        currentlyAttacking = manager.EnemyMonster.CombatStatus;
        manager.EnemyMonster.SetOriginPos(transform);
    }

    private void Update()
    {
        DmgShake(false);
    }

    private void OnMouseDown()
    {
        if (currentlyAttacking == true)
        {
            manager.EnemyMonster.DealDmg(1 * DmgModifier);
            DmgShake(true);
        }
        else
        {
            DmgShake(false);
        }
    }

    private void DmgShake(bool addTime)
    {
        //add time if true (sets the timer to 0)
        if (addTime)
            CounterShake -= 0.25f;

        //sets the timer to 0 so it doesnt go negative.
        if (CounterShake < 0)
            CounterShake = 0;

        //if the counter is bigger than the interval then set it to its max value.
        //when the counter has reached the interval and it has moved then reset its position to its original position.
        if (CounterShake >= intervalShake)
        {
            CounterShake = 0.25f;
            if (HasMoved)
            {
                manager.Enemy.transform.position = manager.EnemyMonster.GetOriginPos();
                HasMoved = false;
            }
        }
        else
        {
            HasMoved = true;
            CounterShake += Time.deltaTime;
            manager.Enemy.transform.position = manager.EnemyMonster.Shake();
        }
    }
}

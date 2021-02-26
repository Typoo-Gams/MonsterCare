using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Toughness_Modifer : MonoBehaviour
{
    GameManager manager;
    //public GameObject enemyMonster;
    bool isActive;

    //this is for the random dmg the enemy does
    public int minDmg = 1;
    public int maxDmg = 10;

    //so that the enemy does dmg every 5 seconds
    public float dmgDelay = 5f;
    //private float dmgTimer = 0f;

    private void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        isActive = false;
    }

    private void Update()
    {
        if(isActive == false)
        {
            isActive = true;
            RandomDmgSpawn();
        }
    }

    //This decides how much dmg the enemy deals
    public void RandomDmgSpawn()
    {
        if(isActive == true)
        {
            manager.Enemy = Instantiate(enemyMonster);
            manager.ActiveMonster.UpdateHealth(Random.Range(minDmg, maxDmg + dmgDelay * Time.deltaTime));
            Debug.Log("is it working");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Toughness_Modifer : MonoBehaviour
{
    //public [Insert player health script here]

    public GameObject enemyPrefab;
    bool isActive;

    public int minDmg = 1;
    public int maxDmg = 10;

    private void Start()
    {
        //playerHealth = 
        isActive = false;
    }

    private void Update()
    {
        if(isActive == false)
        {
            isActive = true;
            RandomSpawn();
        }
    }

    //This decides how much dmg the enemy deals
    //For now this will do...
    public void RandomSpawn()
    {
        if(isActive == true)
        {
            Instantiate(enemyPrefab);
            //playerHealth -= Random.Range(minDmg, maxDmg);
            //Debug.Log(playerHealth);
        }
    }
}
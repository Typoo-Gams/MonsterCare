using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Toughness_Modifer : MonoBehaviour
{
    GameManager manager;
    Canvas canvas;

    string[] enemyMonsterPaths = { "Prefabs/MonsterStuff/Enemy Monsters/FireEnemyPrefab"};
    int pathLengths;

    //checking if its active so that it only spawns the monster once
    bool isActive;

    //this is for the random dmg the enemy does
    public int minDmg = 1;
    public int maxDmg = 15;

    //so that the enemy does dmg every 5 seconds
    public float dmgDelay = 3f;
    float counter;
    //private float dmgTimer = 0f;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("CanvasFighting").GetComponent<Canvas>();
        pathLengths = Random.Range(0, enemyMonsterPaths.Length);

        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        isActive = false;

        /*This spawns the health for our monster and makes sure
            * that the health will go down*/
        Slider Spawned = Instantiate(manager.GreenSliderPrefab);
        manager.ActiveMonster.CombatActive(true);
        Spawned.transform.SetParent(canvas.transform, false);
        Spawned.transform.localScale = new Vector3(2, 2, 2);
        manager.ActiveMonster.AssignHealthBar(Spawned);
        manager.ActiveMonster.DebugMonster();

        isActive = true;
        SpawnEnemy();
    }

    private void Update()
    {
       if(manager.EnemyMonster != null)
       {
            if (isActive == true)
            {
                //This is for the enemy attacking our monster
                counter += Time.deltaTime;
                if (counter > dmgDelay)
                {
                    counter = 0;
                    manager.ActiveMonster.DealDmg(Random.Range(minDmg, maxDmg) + (int)manager.EnemyMonster.ToughnessModifier);
                }
            }
       }
        
    }

    //This decides how much dmg the enemy deals
    public void SpawnEnemy()
    {
        if (isActive == true)
        {
            manager.Enemy = Instantiate(Resources.Load<GameObject>(enemyMonsterPaths[pathLengths]));
        }
    }
}
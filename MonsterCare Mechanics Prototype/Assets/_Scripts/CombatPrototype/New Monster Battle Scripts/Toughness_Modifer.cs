using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Toughness_Modifer : MonoBehaviour
{
    GameManager manager;
    Canvas canvas;

    string[] enemyMonsterPaths = { "Prefabs/MonsterStuff/Enemy Monsters/FireEnemyPrefab",   //0
                                   "Prefabs/MonsterStuff/Enemy Monsters/FireEnemy2Prefab",  //1
                                   "Prefabs/MonsterStuff/Enemy Monsters/EarthEnemyPrefab",  //2
                                   "Prefabs/MonsterStuff/Enemy Monsters/AirEnemyPrefab",    //3
                                   "Prefabs/MonsterStuff/Enemy Monsters/AirEnemy2Prefab",   //4
                                   "Prefabs/MonsterStuff/Enemy Monsters/WaterEnemyPrefab"}; //5

    string monsterPrefab;
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

        string scene = SceneManager.GetActiveScene().name;

        /*This spawns the health for our monster and makes sure
            * that the health will go down*/
        Slider Spawned = Instantiate(manager.GreenSliderPrefab);
        manager.ActiveMonster.CombatActive(true);
        Spawned.transform.SetParent(canvas.transform, false);
        Spawned.transform.localScale = new Vector3(3, 3, 3);
        manager.ActiveMonster.AssignHealthBar(Spawned);
        manager.ActiveMonster.DebugMonster();

        //this checks which scene you are using and then spawns the correct enemy
        switch (scene)
        {
            case "Savannah_FS":
                monsterPrefab = enemyMonsterPaths[0];
                break;
            case "Desert_FS":
                monsterPrefab = enemyMonsterPaths[1];
                break;
            case "Mountain_FS":
                monsterPrefab = enemyMonsterPaths[2];
                break;
            case "Forest_FS":
                monsterPrefab = enemyMonsterPaths[Random.Range(3, 5)];
                break;
            case "Ice_FS":
                monsterPrefab = enemyMonsterPaths[5];
                break;
        }

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

    public void SpawnEnemy()
    {
        if (isActive == true)
        {
            manager.Enemy = Instantiate(Resources.Load<GameObject>(monsterPrefab));
        }
    }
}
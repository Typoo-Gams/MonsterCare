using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class Toughness_Modifer : MonoBehaviour
{
    GameManager manager;
    GameObject canvas;
    public GameObject MonsterHiarchySpawn;

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

    //health indicator//dmg indicator for the player
    public GameObject sprikes, gadient, HealthBar;
    Image rend_Sprikes, rend_Gradient;

    public GameObject GoblinPrefab;

    private void Start()
    {
        rend_Sprikes = sprikes.GetComponent<Image>();
        rend_Gradient = gadient.GetComponent<Image>();
        canvas = GameObject.FindGameObjectWithTag("CanvasFighting");
        pathLengths = Random.Range(0, enemyMonsterPaths.Length);

        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        isActive = false;

        string scene = SceneManager.GetActiveScene().name;

        manager.ActiveMonster.CombatActive(true);

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

        //Spawning an enemy monster
        isActive = true;
        SpawnEnemy();

        //Generating random chance for goblin (1% chance?) bigger chance when he has an inventory
        if (true) 
        {
            if(manager.Enemy == null)
            {
                GameObject GoblinSpawn = Instantiate(GoblinPrefab);
                GoblinSpawn.transform.SetParent(canvas.transform, false);
                Debug.LogWarning("GoblinSpawn");
            }    
        }


        //Sets the DMG effects to their preset.
        float alpha = 1 - (manager.ActiveMonster.HealthStatus / manager.ActiveMonster.GetMaxHealth);
        rend_Gradient.color = new Color(1, 1, 1, alpha);
        rend_Sprikes.color = new Color(1, 1, 1, alpha);

        HealthBar.GetComponent<HealthBarController>().ThisMonster = manager.ActiveMonster;
    }

    private void Update()
    {
       if(manager.EnemyMonster != null && manager.ActiveMonster.HealthStatus != 0)
       {
            if (isActive == true)
            {
                //This is for the enemy attacking our monster
                counter += Time.deltaTime;
                if (counter > dmgDelay)
                {
                    counter = 0;
                    manager.ActiveMonster.DealDmg(Random.Range(minDmg, maxDmg) + (int)manager.EnemyMonster.ToughnessModifier);
                    float alpha = 1 - (manager.ActiveMonster.HealthStatus / manager.ActiveMonster.GetMaxHealth);
                    rend_Gradient.color = new Color(1, 1, 1, alpha);
                    rend_Sprikes.color = new Color(1, 1, 1, alpha);

                    FindObjectOfType<SoundManager>().play("TakingDMG");

                }
            }
       }
    }

    public void SpawnEnemy()
    {
        if (isActive == true)
        {
            manager.Enemy = Instantiate(Resources.Load<GameObject>(monsterPrefab));
            if (MonsterHiarchySpawn != null) 
            {
                manager.Enemy.transform.SetParent(canvas.transform, false);
                manager.Enemy.transform.SetSiblingIndex(MonsterHiarchySpawn.transform.GetSiblingIndex() + 1);
            }
        }
    }
}
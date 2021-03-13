using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


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

    //health indicator//dmg indicator for the player
    public GameObject sprikes, gadient;
    float CounterShake = 0.25f;
    float intervalShake = 0.25f;
    bool HasMoved;
    Vector3 originPos;
    SpriteRenderer rend_Sprikes, rend_Gradient;

    private void Start()
    {
        originPos = sprikes.transform.position;
        rend_Sprikes = sprikes.GetComponent<SpriteRenderer>();
        rend_Gradient = gadient.GetComponent<SpriteRenderer>();
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

                    DmgShake(true);
                    float alpha = 1 - (manager.ActiveMonster.HealthStatus / manager.ActiveMonster.GetMaxHealth);
                    rend_Gradient.color = new Color(1, 1, 1, alpha);
                    rend_Sprikes.color = new Color(1, 1, 1, alpha);

                    FindObjectOfType<SoundManager>().play("TakingDMG");

                }
            }
       }
        DmgShake(false);
    }

    public void SpawnEnemy()
    {
        if (isActive == true)
        {
            manager.Enemy = Instantiate(Resources.Load<GameObject>(monsterPrefab));
        }
    }



    /// <param name="sped">how fast it shakes</param>
    /// <param name="amm">how much it shakes</param>
    /// <returns></returns>
    public Vector3 Shake(float speed = 40.0f, float amount = 0.25f)
    {

        float x = Mathf.Sin(Time.time * speed) * amount;
        float y = Mathf.Sin(Time.time * speed + 0.2f) * amount;

        return new Vector3(x, y, 0);
    }

    //Adds shake Time to monster
    /// <summary>
    /// Shakes the monster's transform from left to right
    /// </summary>
    /// <param name="addTime">True if time should be added, false to update the shake position if it has time.</param>
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
                sprikes.transform.position = originPos;
                HasMoved = false;
            }
        }
        else
        {
            HasMoved = true;
            CounterShake += Time.deltaTime;
            Vector3 shakePos = Shake();
            sprikes.transform.position = shakePos;
        }
    }
}
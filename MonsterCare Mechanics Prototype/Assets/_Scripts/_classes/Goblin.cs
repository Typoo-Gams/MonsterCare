using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    //References
    GameManager manager;
    GameSaver Saver = new GameSaver();
    GameObject canvas;

    public GameObject[] FoodPrefab;
    public GameObject fullInv;

    //Goblin States
    bool isTapped;
    public bool Stunned;
    public bool Vulnerable;
    public bool Defeated;

    //Goblin Stats
    public int HeadHealth = 3;
    int BodyHealth = 5;
    public float Health = 100;

    //Animator
    public Animator goblinAnim;

    //Mechanics
    float cnt;
    float EscapeTimer;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        canvas = GameObject.FindGameObjectWithTag("CanvasFighting");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isTapped);
        if (isTapped) 
        {
            if (Stunned && !Vulnerable)
            {
                //goblinAnim.Play();
                Vulnerable = true;
                HeadHealth = 3;
            }
            if (Vulnerable)
            {
                cnt += Time.deltaTime;
                if (cnt > 3)
                {
                    cnt = 0;
                    Vulnerable = false;
                }
            }
            if (EscapeTimer > 15)
                GoblinEscape();

            if (Health > 0)
                EscapeTimer += Time.deltaTime;

            if (Health <= 0)
                GoblinDefeated();
        }
        else 
        {
            //Check when steal anim is done then escape
        }
    }

    private void StealingFood()
    {
        Saver.AddGoblinInv(manager.FoodReward);
    }

    public void GoblinDefeated()
    {/*
        bool spawned = false;
        for (int i = 0; i < manager.FoodInventory.Length; i++)
        {
            Debug.LogWarning("searching inv");
            if (manager.FoodInventory[i].FoodType == "None")
            {
                GameObject Spawn = Instantiate(FoodPrefab[Random.Range(0, 4)]);
                manager.FoodInventory[i] = Spawn.GetComponent<FoodManager_FoodObject>().ThisFood;
                Spawn.transform.SetParent(canvas.transform, false);
                spawned = true;
                break;
            }
        }
        if (!spawned)
        {
            GameObject Spawn = Instantiate(fullInv);
            Spawn.transform.SetParent(canvas.transform, false);
        }
             */   
        
        Saver.ClearGoblinInv();
        Defeated = true;

        //temporary
        Destroy(gameObject);
    }

    private void GoblinEscape()
    {
        //goblinAnim;
        Debug.LogError("goblin escaped but nothing is happening");
    }

    private void OnMouseDown()
    {
        if (!isTapped)
        {
            isTapped = true;
            Destroy(GetComponent<PolygonCollider2D>()); 
        }
    }
}

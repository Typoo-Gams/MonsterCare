using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterManager_Prototype : MonoBehaviour
{
    //monster stuff
    public Monster StartMonster;
    public Slider SliderPrefab;
    private Canvas CurrentCanvas;

    //shake stuff
    private float intervalShake = 0.25f;
    private float CounterShake = 0.25f;
    private bool HasMoved = false;

    //touch stuff
    Touch touch;



    // Start is called before the first frame update
    void Awake()
    {
    //getting canvas
    CurrentCanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
    //making health bar
    Instantiate(SliderPrefab).transform.SetParent(CurrentCanvas.transform, false);  
    //testing monster class
    StartMonster = new Monster("Ditto");
    StartMonster.DebugStatus();
    StartMonster.AssignHealthBar(GameObject.FindGameObjectWithTag("UnusedSlider").GetComponent<Slider>());
    StartMonster.CombatActive(true);
    StartMonster.SetOriginPos(transform);
    }

    // Update is called once per frame
    void Update()
    {
       //update shake
       DmgShake(false);

        if (StartMonster.HealthStatus <= 0)
        {
           Death();
        }
        if (StartMonster.HealthStatus > 0)
        {
           Revive();
        }
    }
 
 
    private void TouchAttack() 
    {
       if (StartMonster.CombatStatus)
       {
           //Getting Touch input
           if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
           {
               switch (touch.phase)
               {
                   //if touch began deal dmg and add shake
                   case TouchPhase.Began:
 
                       StartMonster.DealDmg(1);
                       DmgShake(true);
                       break;
                   default:
                       break;
               }
           }
           else //update shake
           {
               DmgShake(false);
           }
       }
    }

    //Adds shake Time to monster
    private void DmgShake(bool addTime) 
    {
        if (addTime) 
            CounterShake -= 0.25f; 
        
        if (CounterShake < 0)
            CounterShake = 0;
        
        if (CounterShake >= intervalShake)
        {
            CounterShake = 0.25f;
            if (HasMoved) 
            {
                transform.position = StartMonster.GetOriginPos();
                HasMoved = false;
            }
        }
        else
        {
            HasMoved = true;
            CounterShake += Time.deltaTime;
            transform.position = StartMonster.Shake();
        }
    }

    private void Death()
    {
        transform.rotation = new Quaternion(0f, 0f, 0.707106709f, 0.707106948f);
        StartMonster.CombatActive(false);
    }
    private void Revive()
    {
        transform.rotation = new Quaternion(0f, 0f, 0.0f, 0.0f);
        StartMonster.CombatActive(true);
    }


    private void OnMouseDown()
    {
        TouchAttack();
    }

    
}
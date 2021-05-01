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
    StartMonster.DebugMonster();
    //StartMonster.AssignHealthBar(GameObject.FindGameObjectWithTag("UnusedSlider").GetComponent<Slider>());
    StartMonster.CombatActive(true);
    }

    // Update is called once per frame
    void Update()
    {
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
                       break;
               }
           }
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
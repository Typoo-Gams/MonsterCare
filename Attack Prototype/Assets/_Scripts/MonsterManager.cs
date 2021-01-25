using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterManager : MonoBehaviour
{
    //monster stuff
    Monster StartMonster;
    public Slider SliderPrefab;
    private Canvas CurrentCanvas;

    //shake stuff
    private float intervalShake = 0.25f;
    private float CounterShake = 0.25f;
    private bool HasMoved = false;

    //touch stuff
    Touch touch;



    // Start is called before the first frame update
    void Start()
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
        //touchAttack();

        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                Debug.Log("Something Hit");
                if (raycastHit.collider.name == "Soccer")
                {
                    Debug.Log("Soccer Ball clicked");
                }

                //OR with Tag

                if (raycastHit.collider.CompareTag("SoccerTag"))
                {
                    Debug.Log("Soccer Ball clicked");
                }
            }
        }
    }


    private void touchAttack() 
    {
        //Getting Touch input
        if (Input.touchCount > 0) 
        {
            touch = Input.GetTouch(0);

            switch (touch.phase)
            {

                case TouchPhase.Began:

                    StartMonster.DealDmg(1);
                    DmgShake(true);
                    break;
            }
        }
        else 
        {
            DmgShake(false);
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
                transform.position = StartMonster.resetPosition();
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

    

}


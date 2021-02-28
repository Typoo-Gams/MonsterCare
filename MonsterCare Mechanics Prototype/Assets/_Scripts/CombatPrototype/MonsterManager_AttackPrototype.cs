using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterManager_AttackPrototype : MonoBehaviour
{
    GameManager manager;

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
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();

        //getting canvas
        CurrentCanvas = GameObject.FindGameObjectWithTag("CanvasFighting").GetComponent<Canvas>();
        //making health bar
        Slider healthbarr = Instantiate(SliderPrefab);
        healthbarr.transform.SetParent(CurrentCanvas.transform, false);
        healthbarr.transform.localPosition = new Vector3(0, 110, 0);
        healthbarr.transform.localScale = new Vector3(1.66908312f, 1.66908312f, 1.66908312f);
        //testing monster class
        StartMonster = new Monster("Enemy_Placeholder");
        StartMonster.AssignHealthBar(healthbarr);
        StartMonster.CombatActive(true);
        StartMonster.SetOriginPos(transform);

        manager.EnemyMonster = StartMonster;
        StartMonster.ToughnessModifier = Random.Range(1, 10);
    }

    // Update is called once per frame
    void Update()
    {
        //update shake
        DmgShake(false);
        //updating its visuals for being dead/alive
        if (StartMonster.DeathStatus)
        {
           Death();
        }
        if (StartMonster.DeathStatus != true)
        {
           Revive();
        }
    }
 
 //method to deal dmg if the monster is touched/tapped
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
                        Debug.Log("Attacked");
                        StartMonster.DealDmg(10);
                        DmgShake(true);
                        break;
                    default:
                        Debug.LogError(this.name + " did something weird.");
                        break;
                } 
            }
            else //update shake
            {
                DmgShake(false);
            }
        }
    }

    //Removed the healthbar when the enemy is killed.
    private void OnDestroy()
    {
        Slider destroy = StartMonster.GetHealthbar();
        Destroy(destroy);
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

    //turns off the combat for the enemy monster.
    //visually shows the monster is dead by rotating the sprite so its laying down.
    private void Death()
    {
        //transform.rotation = new Quaternion(0f, 0f, 0.707106709f, 0.707106948f);
        StartMonster.CombatActive(false);
    }

    //turns on the combat for the enemy monster
    //visually shows the monster is alive by rotating it so its standing.
    private void Revive()
    {
        transform.rotation = new Quaternion(0f, 0f, 0.0f, 0.0f);
        StartMonster.CombatActive(true);
    }

    //On mouse down attack
    private void OnMouseDown()
    {
        TouchAttack();
    }
}
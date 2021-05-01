using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MonsterManager_AttackPrototype : MonoBehaviour
{
    GameManager manager;

    //monster stuff
    public Monster StartMonster;
    public GameObject ThisHealthBar;
    GameObject healthbarr;
    public Vector3 HealthbarPos;
    private Canvas CurrentCanvas;
    public string ThisPrefabPath;
    string sceneElement;

    //touch stuff
    Touch touch;


    //TTK Timer
    float TTKcnt;
    Text TTKTimer;


    // Start is called before the first frame update
    void Start()
    {
        TTKTimer = GameObject.FindGameObjectWithTag("TTKTimer").GetComponent<Text>();

        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        sceneElement = SceneManager.GetActiveScene().name;

        //getting canvas
        CurrentCanvas = GameObject.FindGameObjectWithTag("CanvasFighting").GetComponent<Canvas>();
        //making health bar
        healthbarr = Instantiate(ThisHealthBar);
        healthbarr.transform.SetParent(CurrentCanvas.transform, false);
        healthbarr.GetComponent<HealthBarController>().ThisMonster = StartMonster;
        healthbarr.transform.localPosition = HealthbarPos;

        //creating the monster
        StartMonster = new Monster("Enemy_Placeholder");
        StartMonster.AssignHealthBar(healthbarr);
        StartMonster.CombatActive(true);

        switch (sceneElement)
        {
            case "Savannah_FS":
                StartMonster.Element = MonsterElement.Fire;
                break;

            case "Desert_FS":
                StartMonster.Element = MonsterElement.Fire;
                break;

            case "Forest_FS":
                StartMonster.Element = MonsterElement.Air;
                break;

            case "Mountain_FS":
                StartMonster.Element = MonsterElement.Earth;
                break;

            case "Ice_FS":
                StartMonster.Element = MonsterElement.Water;
                break;
        }

        manager.EnemyMonster = StartMonster;
        StartMonster.ToughnessModifier = Random.Range(1, 10);
    }

    // Update is called once per frame
    void Update()
    {
        //Deactivates the monster if health is 0
        if (StartMonster.DeathStatus)
        {
            Death();
        }
        else
        {
            TTKcnt += Time.deltaTime;
            TTKTimer.text = TTKcnt.ToString();
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
                        break;
                    default:
                        Debug.LogError(this.name + " did something weird.");
                        break;
                } 
            }
        }
    }

    //Removed the healthbar when the enemy is killed.
    private void OnDestroy()
    {
        Destroy(healthbarr);
    }

    //turns off the combat for the enemy monster.
    //visually shows the monster is dead by rotating the sprite so its laying down.
    private void Death()
    {
        //transform.rotation = new Quaternion(0f, 0f, 0.707106709f, 0.707106948f);
        StartMonster.CombatActive(false);
    }

    //On mouse down attack
    private void OnMouseDown()
    {
        TouchAttack();
    }
}
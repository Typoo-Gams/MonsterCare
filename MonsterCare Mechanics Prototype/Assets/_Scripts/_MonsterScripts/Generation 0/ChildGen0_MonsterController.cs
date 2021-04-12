using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 
 *  This script is mean to be a temporary monster for testing the mechanics and the functionality  and properties of the monster class.
 *  this script will most likely become a template for how we create all monster scripts in the future.
 * 
 *  these monster scripts might end up being children of the monster class instead of having monster objects in them.
 */


public class ChildGen0_MonsterController : MonoBehaviour
{
    public GameObject Report;
    private GameObject ReportRefference;
    private bool SpawnReport;

    private string prefabLocation = "Prefabs/MonsterStuff/Monsters/Gen 0/Child_Gen0";
    public Monster monster;
    GameSaver Saver = new GameSaver();
    GameManager manager;
    float cnt = 0;
    Animator thisAnimator;
    float cntAnimation;
    public GameObject Smoke;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        thisAnimator = GetComponent<Animator>();
        //Creates a new monster object.
        monster = new Monster("Child_Gen0", prefabLocation);
        //loads the monster stats.
        if (Saver.MonsterObtainedBefore(monster.Name))
        {
            //loads the monster stats.
            Saver.LoadMonster(monster);
        }
        else
        {
            SpawnReport = true;
            //Overwrites the previous monsters saved stats
            Saver.SaveMonster(monster);
            monster.EnergyStatus = 10;
            monster.HungerStatus = 100;
            monster.SleepStatus = 100;
        }
        //updates the monster stats from how much time passed since the last save to simulate things happening while the player isnt playing the game.
        monster.AtGameWakeUp(Saver.FindTimeDifference());
        //Sends the monster object to the gamemanager so that other scripts can easily reference it.
        SendMonster();
        Debug.Log("Current monster: " + this);


        monster.SetReport(Report);

    }


    // Update is called once per frame
    void Update()
    {

        //ths is where stat changes happen
        cnt += Time.deltaTime;
        if (cnt > 0.1f) 
        {
            monster.DegradeHunger();
            monster.UpdateHappiness();
            monster.UpdateSleeping(monster.IsSleepingStatus, 1);
            cnt = 0;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)) Destroy(ReportRefference);

        
        if (SceneManager.GetActiveScene().name == "MonsterHome")
        {
            //checks for devolution
            Devolution();

            //Checks for evolution
            Evolution();

            //checks if a report should be spawned
            if (SpawnReport && manager.Fade.GetCurrentAnimatorStateInfo(0).IsName("New State"))
            {
                GameObject spawn = Instantiate(monster.GetReport());
                spawn.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
                SpawnReport = false;
            }
        }

        
        //changes the sleeping anim state.
        if (monster.IsSleepingStatus)
            gameObject.GetComponent<Animator>().SetBool("Sleeping", monster.IsSleepingStatus);
        else
            gameObject.GetComponent<Animator>().SetBool("Sleeping", monster.IsSleepingStatus);

        //plays the eating anim propperly. maybe this could be done with anim triggers???
        if (thisAnimator.GetBool("Eating"))
        {
            cntAnimation += Time.deltaTime;
            if (thisAnimator.GetCurrentAnimatorStateInfo(0).length <= cntAnimation)
            {
                thisAnimator.SetBool("Eating", false);
                cntAnimation = 0;
            }
        }

    }

    //Save the monster's stats when the gameobject is destroyed.
    private void OnDestroy()
    {
        try 
        {
            if (gameObject.GetComponentInParent<GameManager>().ActiveMonster.PrefabLocation == prefabLocation)
            {
                try
                {
                    Saver.SaveMonster(monster);
                }
                catch
                {
                    Debug.LogWarning("The monster tried to save before it was created.");
                }
            }
        }
        catch 
        {
            Debug.LogWarning("The this monster was destroyed and couldnt use it's gameObject");
        }
    }


    //when the application is closed try to save.
    private void OnApplicationQuit()
    {
        if (monster != null)
            Saver.SaveMonster(monster);
        else
            Debug.LogWarning("Could not save. There was no monster.");
    }

    //when the application is paused save the monster.
    private void OnApplicationPause(bool focus)
    {
        if(monster != null)
            Saver.SaveMonster(monster);
        else
            Debug.LogWarning("Could not save. There was no monster.");
    }


    //Simple prototype example of evolution. these will be different for each monster.
    //Each monster should have the same Evolution() method so that it can be called with sendMessage when the evolution trigger is activated.
    private void Evolution()
    {
        //This is what happens when the monster is evolving.
        //Destroy the current monster object. spawn in the new monster. needs to load the new evolved monster when the game is reopened after being closed.
        if (monster.CanEvolveStatus)
        {
            if (!thisAnimator.GetBool("Evolve"))
            {
                thisAnimator.SetBool("Evolve", true);
                cntAnimation = 0;
                Debug.Log(monster.Name + " Is evolving!!");
                Saver.SaveMonster(monster);
                manager.Fade.Play("EvolutionFadeOut");
            }

            if (!thisAnimator.GetBool("Eating") && !thisAnimator.GetBool("Sleeping")) 
            {
                cntAnimation += Time.deltaTime;
                if (thisAnimator.GetCurrentAnimatorStateInfo(0).length < cntAnimation)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        GameObject spawn = Instantiate(Smoke);
                        spawn.transform.SetParent(transform.parent, false);
                        spawn.transform.position = transform.position;
                    }
                    //Destroy the old monster
                    Destroy(gameObject);
                    //create the new monster
                    GameObject NextEvolution = null;
                    switch (monster.Element)
                    {
                        case "Fire":
                            NextEvolution = Resources.Load<GameObject>("Prefabs/MonsterStuff/Monsters/Gen 1/FireSleepy_Gen1");
                            break;

                        case "Water":
                            NextEvolution = Resources.Load<GameObject>("Prefabs/MonsterStuff/Monsters/Gen 1/WaterPlayful_Gen1");
                            break;

                        case "Earth":
                            NextEvolution = Resources.Load<GameObject>("Prefabs/MonsterStuff/Monsters/Gen 1/BeefMaster_Gen1");
                            break;

                        case "Air":
                            NextEvolution = Resources.Load<GameObject>("Prefabs/MonsterStuff/Monsters/Gen 1/AirSleepy_Gen1");
                            break;
                    }
                    GameObject Spawned = Instantiate(NextEvolution);
                    Spawned.transform.SetParent(transform.parent, false);
                }
            }
        }
    }

    private void Devolution()
    {
        if (monster.DeathStatus)
        {
            //This is what happens when the monster is fainted.
            //Destroy the current monster object. spawn in the new monster. needs to load the new evolved monster when the game is reopened after being closed. clears the save file with an empty monster
            Monster empty = new Monster("empty", "None");
            Saver.SaveMonster(empty);
            GameObject NextEvolution = Resources.Load<GameObject>("Prefabs/MonsterStuff/Monsters/Gen 0/Child_Gen0");
            GameObject Parent = GameObject.Find("__app").GetComponentInChildren<GameManager>().gameObject;
            Destroy(gameObject);
            GameObject SpawnedMonster = Instantiate(NextEvolution);
            SpawnedMonster.transform.SetParent(Parent.transform, false);
            Saver.SaveMonster(SpawnedMonster.GetComponent<ChildGen0_MonsterController>().monster);
        }
    }


    //Send this monster to the GameManager
    void SendMonster() 
    {
        SendMessageUpwards("GetMonster", monster);
        SendMessageUpwards("GetMonster", gameObject);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AirSleepyGen1_MonsterController : MonoBehaviour
{
    public GameObject Report;
    private GameObject ReportRefference;
    private bool SpawnReport;

    private string prefabLocation = "Prefabs/MonsterStuff/Monsters/Gen 1/AirSleepy_Gen1";
    public Monster monster;
    GameManager manager;
    GameSaver Saver = new GameSaver();
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
        monster = new Monster("AirSleepy_Gen1", prefabLocation);
        //Checks if this monster is a new evolution or not then loads the monster info or overwrites the old monster's saved stats with the new one.
        if (Saver.MonsterObtainedBefore(monster.Name))
        {
            //loads the monster stats.
            Saver.LoadMonster(monster);
        }
        else
        {
            SpawnReport = true;
            //Overwrites the previous monsters saved stats
            Saver.LoadMonster(monster);
            monster.HealthStatus = monster.GetMaxHealth;
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
        cnt += Time.deltaTime;
        if (cnt >= 0.1f)
        {
            monster.DegradeHunger();
            monster.UpdateHappiness();
            monster.UpdateSleeping(monster.IsSleepingStatus, 1);
            cnt = 0;
        }
        if (SceneManager.GetActiveScene().name == "MonsterHome")
        {
            //checks for devolution
            Devolution();

            //Checks for evolution
            Evolution();

            //checks if a report should be spawned
            if (SpawnReport && manager.Fade.GetCurrentAnimatorStateInfo(0).IsName("New State") && monster.GetReport() != null)
            {
                GameObject spawn = Instantiate(monster.GetReport());
                spawn.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
                SpawnReport = false;
                manager.HideUI = true;
            }
        }


        if (monster.IsSleepingStatus)
            gameObject.GetComponent<Animator>().SetBool("Sleeping", monster.IsSleepingStatus);
        else
            gameObject.GetComponent<Animator>().SetBool("Sleeping", monster.IsSleepingStatus);

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


    //when the application is closed try to save.
    private void OnApplicationQuit()
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


    //when the application is paused save the monster.
    private void OnApplicationPause(bool focus)
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
                //thisAnimator.SetBool("Evolve", true);
                cntAnimation = 0;
                Saver.SaveMonster(monster);
                manager.Fade.Play("EvolutionFadeOut");
                Debug.Log(monster.Name + " Is evolving!!");
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

                    //Destroy(gameObject);

                    //create the new monster
                    GameObject NextEvolution = null;
                    switch (monster.Element)
                    {
                        /*case "Fire":
                            break;

                        case "Water":
                            break;

                        case "Earth":
                            break;

                        case "Air":
                            break;*/
                        default:
                            Debug.LogError("This monster doesnt have any evolutions yet");
                            monster.CanEvolveStatus = false;
                            break;
                    }
                    /*
                    GameObject Spawned = Instantiate(NextEvolution);
                    Spawned.transform.SetParent(transform.parent, false);
                    manager.ActiveMonster.PreviousEvolution = prefabLocation;
                    */
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
        }
    }


    //Send this monster to the GameManager
    void SendMonster()
    {
        SendMessageUpwards("GetMonster", monster);
        SendMessageUpwards("GetMonster", gameObject);
    }
}

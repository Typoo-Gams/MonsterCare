using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

abstract public class MonsterController : MonoBehaviour
{

    [HideInInspector] public GameSaver Saver = new GameSaver();
    private GameObject ReportRefference;
    private bool SpawnReport;
    [HideInInspector] public GameManager manager;
    private float cnt = 0;
    [HideInInspector] public Animator thisAnimator;
    [HideInInspector] public float cntAnimation;

    //These needs to be something
    [HideInInspector]
    public Monster monster; // Monster should be handled by the monster script. the rest should be set in the inspector
    public MonsterType _type;
    public string MonsterName;
    public string _prefabLocation = "Set Me To Something";
    public string devolutionName, devolutionPath;
    public GameObject Smoke;
    public GameObject Report;


    // Start is called before the first frame update
    void Start()
    {
        monster = new Monster(MonsterName, _prefabLocation, _type);
        //loads the monster stats.
        if (Saver.MonsterObtainedBefore(monster.Name))
        {
            //loads the monster stats.
            Saver.LoadMonster(monster);
        }
        else
        {
            //Gets the game ready to spawn a report
            SpawnReport = true;
            //Keeps previous evolution's stats
            Saver.SaveMonster(monster);
        }
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        thisAnimator = GetComponent<Animator>();
        //Creates a new monster object.

        //Sends the monster object to the gamemanager so that other scripts can easily reference it.
        SendMonster();
        //Debug.Log("Current monster: " + this);

        monster.DebugMonster();
        monster.SetReport(Report);

        if (!manager.NewSave && Saver.FindTimeDifference() > 0 && monster.PreviousEvolution == "")
        {
            monster.AtGameWakeUp(Saver.FindTimeDifference());
        }
    }

    // Update is called once per frame
    void Update()
    {

        //ths is where stat changes happen
        cnt += Time.deltaTime;
        if (cnt > manager.MonsterUpdateSpeed)
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
            if (SpawnReport && manager.Fade.GetCurrentAnimatorStateInfo(0).IsName("New State") && monster.GetReport() != null)
            {
                GameObject spawn = Instantiate(monster.GetReport());
                spawn.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
                SpawnReport = false;
                manager.HideUI = true;
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
            if (gameObject.GetComponentInParent<GameManager>().ActiveMonster.PrefabLocation == _prefabLocation)
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
        if (monster != null)
            Saver.SaveMonster(monster);
        else
            Debug.LogWarning("Could not save. There was no monster.");
    }

    //Abstract so that monsters can have different evolution conditions
    abstract public void Evolution();

    public bool IsEvolutionAnimDone()
    {
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
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void Devolution()
    {
        if (monster.DeathStatus)
        {
            //This is what happens when the monster is fainted.
            //Destroy the current monster object. spawn in the new monster. needs to load the new evolved monster when the game is reopened after being closed. clears the save file with an empty monster
            Monster empty = new Monster(devolutionName, devolutionPath);
            empty.DebugMonster();
            Saver.SaveMonster(empty);
            GameObject NextEvolution = Resources.Load<GameObject>(devolutionPath);
            GameObject Parent = GameObject.Find("__app").GetComponentInChildren<GameManager>().gameObject;
            Destroy(gameObject);
            GameObject SpawnedMonster = Instantiate(NextEvolution);
            SpawnedMonster.transform.SetParent(Parent.transform, false);
            //Saver.SaveMonster(SpawnedMonster.GetComponent<ChildGen0_MonsterController>().monster);
            manager.ActiveMonster.PreviousEvolution = _prefabLocation;
            Debug.Log("Monster died and devolved");
        }
    }


    //Send this monster to the GameManager
    void SendMonster()
    {
        SendMessageUpwards("GetActiveMonster", monster);
        SendMessageUpwards("GetObjMonster", gameObject);
    }
}
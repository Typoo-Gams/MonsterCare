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


public class DefaultStarting_MonsterController : MonoBehaviour
{
    public GameObject Report;
    private GameObject ReportRefference;

    private string prefabLocation = "Prefabs/MonsterStuff/Monsters/Gen 0/DefaultStartingMonster";
    public Monster monster;
    GameSaver Saver = new GameSaver();
    float cnt = 0;


    // Start is called before the first frame update
    void Start()
    {

        //Creates a new monster object.
        monster = new Monster("load", prefabLocation);
        //loads the monster stats.
        if (monster.PrefabLocation == Saver.GetMonsterPrefab())
        {
            //loads the monster stats.
            Saver.LoadMonster(monster);
        }
        else
        {
            //Overwrites the previous monsters saved stats
            Saver.SaveMonster(monster);
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


        //Don't work because of preload
        /*
        if (monster.PrefabLocation != Saver.GetMonsterPrefab() && SceneManager.GetActiveScene().name == "MonsterHome")
            ReportRefference = Instantiate(monster.GetReport());
        */
        Evolution();
        if (SceneManager.GetActiveScene().name == "MonsterHome")
        {
            Devolution();
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
            GameObject Parent = GameObject.Find("__app").GetComponentInChildren<GameManager>().gameObject;

            Debug.Log(monster.Name + " Is evolving!!");

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
            Spawned.transform.SetParent(Parent.transform, false);
        }
    }

    private void Devolution()
    {
        if (monster.DeathStatus)
        {
            //This is what happens when the monster is fainted.
            //Destroy the current monster object. spawn in the new monster. needs to load the new evolved monster when the game is reopened after being closed.
            GameObject NextEvolution = Resources.Load<GameObject>("Prefabs/MonsterStuff/Monsters/Gen 0/DefaultStartingMonster");
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
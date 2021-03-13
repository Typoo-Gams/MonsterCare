using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FireSleepyGen1_MonsterController : MonoBehaviour
{
    public GameObject Report;
    private GameObject ReportRefference;

    public Monster monster;
    GameSaver Saver = new GameSaver();
    float cnt = 0;


    // Start is called before the first frame update
    void Start()
    {


        //Creates a new monster object.
        monster = new Monster("FireSleepyGen1", "Prefabs/MonsterStuff/Monsters/Gen 1/FireSleepy_Gen1");
        //Checks if this monster is a new evolution or not then loads the monster info or overwrites the old monster's saved stats with the new one.
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
        cnt += Time.deltaTime;
        if (cnt >= 0.1f)
        {
            monster.DegradeHunger();
            monster.UpdateHappiness();
            monster.UpdateSleeping(monster.IsSleepingStatus, 1);
            cnt = 0;
        }
        Evolution();
        if (SceneManager.GetActiveScene().name == "MonsterHome")
        {
            Devolution();
        }

        //Don't work because of Preload
        if (monster.PrefabLocation != Saver.GetMonsterPrefab() && SceneManager.GetActiveScene().name == "MonsterHome")
            ReportRefference = Instantiate(monster.GetReport());
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

            //Destroy(gameObject);

            //create the new monster
            GameObject NextEvolution;
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
                    break;
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

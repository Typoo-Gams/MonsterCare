using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private string prefabLocation = "Prefabs/MonsterStuff/Monsters/DefaultStartingMonster";
    public Monster monster;
    GameSaver Saver = new GameSaver();
    float cnt = 0;


    // Start is called before the first frame update
    void Start()
    {

        //Creates a new monster object.
        monster = new Monster("load", prefabLocation);
        //loads the monster stats.
        Saver.LoadMonster(monster);
        monster.DeathStatus = false;
        //Remove this when we dont need the healing anymore
        Debug.LogError("remove UpdateHealth from start in " + this);
        monster.UpdateHealth(100f);
        //debugs the monster stats.
        monster.DebugMonster();
        //updates the monster stats from how much time passed since the last save to simulate things happening while the player isnt playing the game.
        monster.AtGameWakeUp(Saver.FindTimeDifference());
        monster.DebugMonster();
        //Sends the monster object to the gamemanager so that other scripts can easily reference it.
        SendMonster();
        Debug.Log("Current monster: " + this);


        monster.SetReport(Report);

        if (monster.PrefabLocation != Saver.GetMonsterPrefab())
            ReportRefference = Instantiate(monster.GetReport());

    }


    // Update is called once per frame
    void Update()
    {
        cnt += Time.deltaTime;
        if (cnt > 1) 
        {
            monster.DegradeHunger();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)) Destroy(ReportRefference);
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
            Debug.Log(monster.Name + " Is evolving!!");

            //This loads and positions the new evolution monster prefab
            GameObject NextEvolution = Resources.Load<GameObject>("Prefabs/MonsterStuff/Monsters/SecondEvolutionMonster");
            Debug.Log(NextEvolution);
            GameObject Parent = GameObject.Find("__app").GetComponentInChildren<GameManager>().gameObject;
            //Destroy the old monster
            Destroy(gameObject);
            //create the new monster
            GameObject Spawned = Instantiate(NextEvolution);
            Spawned.transform.SetParent(Parent.transform, false);
            Spawned.transform.localPosition = new Vector3(1.17614102f, -0.730000019f, 121.02121f);
        }
    }



    //Send this monster to the GameManager
    void SendMonster() 
    {
        SendMessageUpwards("GetMonster", monster);
        SendMessageUpwards("GetMonster", gameObject);
    }
}
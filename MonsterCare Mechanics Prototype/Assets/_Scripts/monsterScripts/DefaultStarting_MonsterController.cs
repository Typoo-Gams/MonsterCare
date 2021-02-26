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
        //difference in time since last time. (in seconds)
        Debug.Log("difference: " + Saver.FindTimeDifference());
        //debugs the monster stats.
        monster.DebugMonster();
        //updates the monster stats from how much time passed since the last save to simulate things happening while the player isnt playing the game.
        monster.AtGameWakeUp(Saver.FindTimeDifference());
        monster.DebugMonster();
        //Sends the monster object to the gamemanager so that other scripts can easily reference it.
        SendMonster(monster);
        Debug.Log("loaded Monster");
    }
    

    // Update is called once per frame
    void Update()
    {
        cnt += Time.deltaTime;
        if (cnt > 1) 
        {
            monster.DegradeHunger();
        }
        Evolution();
    }

    //Save the monster's stats when the gameobject is destroyed.
    private void OnDestroy()
    {
        try
        {
            Saver.SaveMonster(monster);
            Debug.Log("saved monster");
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
            GameObject Spawned = Instantiate(NextEvolution);
            Spawned.transform.SetParent(Parent.transform, false);
            
            //Destroy the old monster
            Destroy(gameObject);
        }
    }



    //Send this monster to the GameManager
    void SendMonster(Monster thisMonster) 
    {
        SendMessageUpwards("GetMonster", thisMonster);
        SendMessageUpwards("GetMonster", gameObject);
    }
}

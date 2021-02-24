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
    public Monster monster;
    GameSaver Saver = new GameSaver();
    float cnt = 0;


    // Start is called before the first frame update
    void Start()
    {
        //Creates a new monster object.
        monster = new Monster("load");
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
    private void Evolutution()
    {

        //this is meant to show an example of how the evolution conditions is meet. these conditions will be different for each monster.
        bool Condition1 = false;
        bool Condition2 = false;

        if (monster.IsFullStatus)
        {
            Condition1 = true;
        }
        if (monster.IsRestedStatus) 
        {
            Condition2 = true;
        }

        //This is what happens when the monster is evolving.
        //Destroy the current monster object. spawn in the new monster. needs to load the new evolved monster when the game is reopened after being closed.
        if (Condition1 && Condition2)
        {
            Debug.Log(monster.Name + " Is evolving!!");
            Sprite NextEvolution = Resources.Load<Sprite>("Sprites/monscarebatowl");
            Debug.Log(NextEvolution);
            gameObject.GetComponent<SpriteRenderer>().sprite = NextEvolution;
        }
    }



    //Send this monster to the GameManager
    void SendMonster(Monster thisMonster) 
    {
        SendMessageUpwards("GetMonster", thisMonster);
        SendMessageUpwards("GetMonster", gameObject);
    }
}

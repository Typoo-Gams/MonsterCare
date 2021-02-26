using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEvolution_MonsterController : MonoBehaviour
{
    public Monster monster;
    GameSaver Saver = new GameSaver();
    float cnt = 0;


    // Start is called before the first frame update
    void Start()
    {
        //Creates a new monster object.
        monster = new Monster("Evolution", "Prefabs/MonsterStuff/Monsters/SecondEvolutionMonster");
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
        monster.DebugMonster();
        //updates the monster stats from how much time passed since the last save to simulate things happening while the player isnt playing the game.
        monster.AtGameWakeUp(Saver.FindTimeDifference());
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
            GameObject NextEvolution = Resources.Load<GameObject>("Prefabs/MonsterStuff/Monsters/SecondEvolutionMonster");
            Debug.Log(NextEvolution);
            GameObject Parent = GameObject.Find("__app").GetComponentInChildren<GameManager>().gameObject;
            Instantiate(NextEvolution, NextEvolution.transform.position, Quaternion.identity, Parent.transform);
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

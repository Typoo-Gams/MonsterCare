using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultStarting_MonsterController : MonoBehaviour
{
    public Monster monster;
    GameSaver Saver = new GameSaver();
    float cnt = 0;


    // Start is called before the first frame update
    void Start()
    {
        monster = new Monster("load");
        Saver.LoadMonster(monster);
        Debug.Log("difference: " + Saver.FindTimeDifference());
        monster.DebugMonster();
        monster.AtGameWakeUp(Saver.FindTimeDifference());
        monster.DebugMonster();
        SendMonster(monster);
        Evolutution();
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


    //Simple prototype example of evolution
    private void Evolutution()
    {
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

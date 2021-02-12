using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultStarting_MonsterController : MonoBehaviour
{
    public Monster monster;
    GameSaver Saver = new GameSaver();
    float cnt = 0;

    private void Awake()
    {
        monster = new Monster("load");
        Saver.LoadMonster(monster);
        monster.AtGameWakeUp(Saver.FindTimeDifference());
        SendMonster(monster);
    }

    // Start is called before the first frame update
    void Start()
    {
        Evolutution();

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
        Saver.SaveMonster(monster);
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
        //get gameobject to gamemanager
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultStarting_MonsterController : MonoBehaviour
{
    public Monster monster;
    GameSaver Saver = new GameSaver();


    private void Awake()
    {
        monster = new Monster("load");
        Saver.LoadMonster(monster);
    }

    // Start is called before the first frame update
    void Start()
    {
        monster.DebugMonster();
        monster.AtGameWakeUp(Saver.FindTimeDifference());
        monster.DebugMonster();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Saver.SaveMonster(monster);
    }


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
}

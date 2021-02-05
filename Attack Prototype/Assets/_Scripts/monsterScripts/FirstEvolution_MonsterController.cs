using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEvolution_MonsterController : MonoBehaviour
{
    public Monster monster;
    public int TimeToSleep = 0;
    float interval = 1;
    public float cnt = 0;

    // Start is called before the first frame update
    void Awake()
    {
        monster = new Monster("Default");
        monster.UpdateHunger(5);
        monster.DebugStatus();
    }


    // Update is called once per frame
    void Update()
    {
        if (TimeToSleep > 0)
        {
            cnt += Time.deltaTime;
            if (cnt > interval)
            {
                Sleeping();
                cnt = 0;
            }
        }
    }


    private void Evolutution()
    {
        bool Condition1 = false;
        bool Condition2 = false;

        if (monster.IStarvingStatus)
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


    private void Sleeping()
    { 
        if(TimeToSleep > 0)
        {
            TimeToSleep--;
            monster.UpdateSleep(true);
            monster.UpdateHunger(0);
        }
        if (monster.IsRestedStatus)
        {
            Evolutution();
        }
    }
}

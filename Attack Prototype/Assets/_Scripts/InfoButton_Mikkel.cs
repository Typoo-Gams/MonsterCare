using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoButton_Mikkel : MonoBehaviour
{
    private static int HealthStat;              //Static variable to hold our stat?

    public DefaultStarting_MonsterController Monster;
    public Monster ThisMonster;
    public Text TextStatBoard;

    // Start is called before the first frame update
    void Start()
    {
        ThisMonster = Monster.monster;

        Debug.Log(string.Format("Health: {0} \n Hunger: {1} \n Sleep: {2} ", ThisMonster.HealthStatus, ThisMonster.HungerStatus, ThisMonster.SleepStatus));

        TextStatBoard.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        TextStatBoard.text = "Health: " + ThisMonster.HealthStatus + "\n Hunger:" + ThisMonster.HungerStatus + "\n Sleep:" + ThisMonster.SleepStatus;
    }

    public void StatBoard()
    {
        if(TextStatBoard.enabled == true)
        {
            TextStatBoard.enabled = false;
        }
        else
        {
            TextStatBoard.enabled = true;
        }
    }
   
}                            
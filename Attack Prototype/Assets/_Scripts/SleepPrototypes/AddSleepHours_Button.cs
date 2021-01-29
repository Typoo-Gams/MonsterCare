using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddSleepHours_Button : MonoBehaviour
{

    DefaultStarting_MonsterController ThisMonster;
    Button thisButton;


    // Start is called before the first frame update
    void Start()
    {
        ThisMonster = GameObject.FindGameObjectWithTag("Monster").GetComponent<DefaultStarting_MonsterController>();
        thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(TaskOnClick);
    }


    void TaskOnClick() 
    {
        if ((ThisMonster.monster.SleepMaxBorder - ThisMonster.monster.SleepStatus) + 1 > ThisMonster.TimeToSleep) 
        {
            ThisMonster.TimeToSleep += 1;
        }
    }
}
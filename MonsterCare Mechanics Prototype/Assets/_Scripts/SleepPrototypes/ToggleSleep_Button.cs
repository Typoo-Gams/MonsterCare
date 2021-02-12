using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSleep_Button : MonoBehaviour
{
    public Image NightTime;
    DefaultStarting_MonsterController ThisMonster;
    public Text zZz;
    Button thisButton;
    float counter;

    // Start is called before the first frame update
    void Start()
    {
        NightTime.enabled = false;
        ThisMonster = GameObject.FindGameObjectWithTag("Monster").GetComponent<DefaultStarting_MonsterController>();
        thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(TaskOnClick);
    }

    private void Update()
    {
        counter += Time.deltaTime;
        if (counter > 1) 
            ThisMonster.monster.UpdateSleeping(ThisMonster.monster.IsSleepingStatus);    
    }

    void TaskOnClick() 
    {
        NightTime.enabled = !NightTime.enabled;
        ThisMonster.monster.IsSleepingStatus = !ThisMonster.monster.IsSleepingStatus;
        ThisMonster.monster.DebugMonster();
        zZz.gameObject.SetActive(ThisMonster.monster.IsSleepingStatus);
    }
}
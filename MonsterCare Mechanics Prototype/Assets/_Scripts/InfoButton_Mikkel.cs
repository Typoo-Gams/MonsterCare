using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoButton_Mikkel : MonoBehaviour
{
    public DefaultStarting_MonsterController Monster;
    public Monster ThisMonster;
    public GameObject StatBoard;
    public Text TextStatBoard;
    bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        Monster = GameObject.FindGameObjectWithTag("Monster").GetComponent<DefaultStarting_MonsterController>();
        gameObject.GetComponent<Button>().onClick.AddListener(StatBoardToggle);
        ThisMonster = Monster.monster;
        
        isActive = false;
        StatBoard.SetActive(isActive);
    }

    // Update is called once per frame
    void Update()
    {
        string text = string.Format("HomePrototype\nHealth: {0}\n Hunger: {1}\n Sleep: {2}", 
                            ThisMonster.HealthStatus, ThisMonster.HungerStatus, ThisMonster.SleepStatus);
        TextStatBoard.text = text;
    }

    public void StatBoardToggle()
    {
        if (isActive)
        {
            isActive = false;
            StatBoard.SetActive(isActive);
        }
        else
        {
            isActive = true;
            StatBoard.SetActive(isActive);
        }
    }
}                       
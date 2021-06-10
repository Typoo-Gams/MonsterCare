using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheats_SetMonster1_Button : MonoBehaviour
{
    GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(TaskOnClick);
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
    }

    private void TaskOnClick()
    {
        manager.ActiveMonster.HealthStatus = manager.ActiveMonster.GetMaxHealth/100;
        manager.ActiveMonster.HungerStatus = manager.ActiveMonster.GetMaxHunger/100;
        manager.ActiveMonster.EnergyStatus = manager.ActiveMonster.GetMaxEnergy/100;
        manager.ActiveMonster.HappinessStatus = manager.ActiveMonster.GetMaxHappiness/100;
        manager.ActiveMonster.SleepStatus = manager.ActiveMonster.GetMaxSleep/100;
    }
}

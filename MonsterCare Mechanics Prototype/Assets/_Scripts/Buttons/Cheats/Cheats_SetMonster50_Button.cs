using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheats_SetMonster50_Button : MonoBehaviour
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
        manager.ActiveMonster.HealthStatus = manager.ActiveMonster.GetMaxHealth/2;
        manager.ActiveMonster.HungerStatus = manager.ActiveMonster.GetMaxHunger/2;
        manager.ActiveMonster.EnergyStatus = manager.ActiveMonster.GetMaxEnergy/2;
        manager.ActiveMonster.HappinessStatus = manager.ActiveMonster.GetMaxHappiness/2;
        manager.ActiveMonster.SleepStatus = manager.ActiveMonster.GetMaxSleep/2;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatController : MonoBehaviour
{

    //
    public Image[] IconBars = new Image[5];
    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();

        IconBars[0].fillAmount = manager.ActiveMonster.HealthStatus / manager.ActiveMonster.GetMaxHealth;
        IconBars[1].fillAmount = manager.ActiveMonster.EnergyStatus / manager.ActiveMonster.GetMaxEnergy;
        IconBars[2].fillAmount = manager.ActiveMonster.HungerStatus / manager.ActiveMonster.GetMaxHunger;
        IconBars[3].fillAmount = manager.ActiveMonster.SleepStatus / manager.ActiveMonster.GetMaxSleep;
        //IconBars[4].fillAmount = manager.ActiveMonster.HappinessStatus / manager.ActiveMonster.GetMaxHappiness;
    }

    // Update is called once per frame
    void Update()
    {
        IconBars[0].fillAmount = manager.ActiveMonster.HealthStatus / manager.ActiveMonster.GetMaxHealth;
        IconBars[1].fillAmount = manager.ActiveMonster.EnergyStatus / manager.ActiveMonster.GetMaxEnergy;
        IconBars[2].fillAmount = manager.ActiveMonster.HungerStatus / manager.ActiveMonster.GetMaxHunger;
        IconBars[3].fillAmount = manager.ActiveMonster.SleepStatus / manager.ActiveMonster.GetMaxSleep;
        //IconBars[4].fillAmount = manager.ActiveMonster.HappinessStatus / manager.ActiveMonster.GetMaxHappiness;
    }
}

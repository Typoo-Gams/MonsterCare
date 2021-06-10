using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Cheats_KillMonster_Button : MonoBehaviour
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
        manager.ActiveMonster.HealthStatus = 1;
        manager.ActiveMonster.HungerStatus = 0;
    }
}

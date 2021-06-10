using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheats_SetEvolveEnergy_Button : MonoBehaviour
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
        manager.ActiveMonster.EnergyStatus = manager.ActiveMonster.GetEvolveCost+1;
    }
}

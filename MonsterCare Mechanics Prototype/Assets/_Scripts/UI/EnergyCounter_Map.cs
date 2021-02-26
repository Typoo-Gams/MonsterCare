using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnergyCounter_Map : MonoBehaviour
{
    GameManager manager;
    Text EnergyCnt;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        EnergyCnt = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        EnergyCnt.text = "Energy: " + Math.Truncate(manager.ActiveMonster.EnergyStatus);
    }
}

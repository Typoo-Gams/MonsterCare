using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyCost_Button : MonoBehaviour
{
    GameManager manager;
    Button ThisButton;
    public int cost;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        ThisButton = gameObject.GetComponent<Button>();
        ThisButton.onClick.AddListener(PayCost);
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.ActiveMonster.EnergyStatus > cost)
            ThisButton.interactable = true;
        else
            ThisButton.interactable = false;

        if(manager.ActiveMonster.HealthStatus == 0)
        {
            ThisButton.interactable = false;
        }
    }   

    void PayCost() 
    {
        manager.ActiveMonster.EnergyStatus -= cost;
    }
}

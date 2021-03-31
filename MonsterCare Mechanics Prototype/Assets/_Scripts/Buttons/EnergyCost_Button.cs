using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyCost_Button : MonoBehaviour
{
    GameManager manager;
    Button ThisButton;
    public int cost;
    public GameObject[] elements;
    public bool paying;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        ThisButton = gameObject.GetComponent<Button>();
        if(paying)
            ThisButton.onClick.AddListener(PayCost);

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (manager.ActiveMonster.EnergyStatus > cost)
        {
            ThisButton.interactable = true;
            foreach(GameObject Icons in elements)
            {
                Icons.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
        else
        {
            ThisButton.interactable = false;
            foreach (GameObject Icons in elements)
            {
                Icons.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            }
        }

        if(manager.ActiveMonster.HealthStatus == 0)
        {
            ThisButton.interactable = false;

        }
    }   

    void PayCost() 
    {
        manager.ActiveMonster.EnergyStatus -= cost;
        FindObjectOfType<SoundManager>().play("ButtonClick");
    }
}

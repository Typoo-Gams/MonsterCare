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
    public bool paying, clicked, loading;
    SceneChanger_Button Changer;
    public Button[] otherButtons;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        ThisButton = gameObject.GetComponent<Button>();
        if(paying)
            ThisButton.onClick.AddListener(PayCost);
    }

    // Update is called once per frame
    void update()
    {
        if (!clicked && !loading)
        {
            if (manager.ActiveMonster.EnergyStatus > cost)
            {
                ThisButton.interactable = true;
                foreach (GameObject Icons in elements)
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

            if (manager.ActiveMonster.HealthStatus == 0)
            {
                ThisButton.interactable = false;
            }
        }
        else
        {
            loading = true;
            ThisButton.interactable = false;
            foreach (Button found in otherButtons)
            {
                found.interactable = false;
            }
            foreach (GameObject Icons in elements)
            {
                Icons.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            }
        }
    }   



    void PayCost() 
    {
        clicked = true;
        manager.ActiveMonster.EnergyStatus -= cost;
        FindObjectOfType<SoundManager>().play("ButtonClick");
    }
}

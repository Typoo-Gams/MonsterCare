using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel_Button : MonoBehaviour
{

    bool isActive;
    public GameObject StatBoard;
    public GameManager manager;
    public Text Stats;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(StatBoardToggle);
        isActive = false;
        StatBoard.SetActive(isActive);
    }

    // Update is called once per frame
    void Update()
    {
        string text = string.Format("HomePrototype\nHealth: {0}\n Hunger: {1}\n Sleep: {2}",
                    (int)manager.ActiveMonster.HealthStatus, (int)manager.ActiveMonster.HungerStatus, (int)manager.ActiveMonster.SleepStatus);
        Stats.text = text;
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

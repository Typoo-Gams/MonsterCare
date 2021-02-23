using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel_Button : MonoBehaviour
{
    //Statboard is active
    bool isActive;
    public GameObject StatBoard;
    public GameManager manager;
    public Text Stats;
    GameSaver saver = new GameSaver();
    string lastPlay;
    float lastPlayInSec;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        Debug.Log(manager.gameObject.name);
        //add fuction StatBoardToggle to click
        gameObject.GetComponent<Button>().onClick.AddListener(StatBoardToggle);
        //set the statboard's isActive to false.
        isActive = false;
        StatBoard.SetActive(isActive);
        
        //Makes a string that contains last date and time the game was saved.
        for (int i = 0; i < saver.LoadTime().Length; i++) 
        {
            if (i == 3)
                lastPlay += "   ";
            if (i < 3)
                lastPlay += saver.LoadTime(i) + ":";
            else
                lastPlay += saver.LoadTime(i) + "/";
        }
        //setting the time difference.
        lastPlayInSec = saver.FindTimeDifference();
    }

    // Update is called once per frame
    void Update()
    {
        //formats the display string
        string text = string.Format("HomePrototype\nHealth: {0}\n Hunger: {1}\n Sleep: {2}\nLast play session ended at: \n{3}\nTime in seconds since last played: {4}",
                    (int)manager.ActiveMonster.HealthStatus, (int)manager.ActiveMonster.HungerStatus, (int)manager.ActiveMonster.SleepStatus, lastPlay, (int)lastPlayInSec);
        Stats.text = text;
    }

    //toggle the active state of the statboard.
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
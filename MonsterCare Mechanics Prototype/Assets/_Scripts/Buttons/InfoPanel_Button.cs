using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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
        //Truncate removes the decimal numbers so the display shows only whole numbers. this is because webGl doesnt like converting float to int with using (int)float.
        if (manager.DevBuild) 
        {
            string text = string.Format("HomePrototype\nHealth: {0}\nEnergy: {1}\n Hunger: {2}\n Sleep: {3}\nLast save at: \n{4}\nTime in seconds since last played: {5}\nHappiness: {6}\nLast element eaten: {7}",
                        Math.Truncate(manager.ActiveMonster.HealthStatus), Math.Truncate(manager.ActiveMonster.EnergyStatus), Math.Truncate(manager.ActiveMonster.HungerStatus), Math.Truncate(manager.ActiveMonster.SleepStatus), lastPlay, Math.Truncate(lastPlayInSec), Math.Truncate(manager.ActiveMonster.HappinessStatus), manager.ActiveMonster.Element);
            Stats.text = text;
        }
        
    }

    //toggle the active state of the statboard.
    public void StatBoardToggle()
    {
        if (manager.DevBuild)
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
            FindObjectOfType<SoundManager>().play("ButtonClick");
        }
    }
}

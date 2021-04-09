using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSleep_Button : MonoBehaviour
{
    public bool PlaySounds;

    public GameObject NightTime;
    public GameManager manager;
    Button thisButton;
    float counter;
    bool toggle;
    GameSaver saver = new GameSaver();
    public GameObject SleepZs;

    public Animator foodInv;



    // Start is called before the first frame update
    void Start()
    {
        //finds the __app in order to reference the gamemanager.
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        //creates the shade and sets it to inactive, parents it under the canvas and transforms its position under the new parent.
        toggle = manager.SleepMemory;
        NightTime = Instantiate(NightTime);
        //sets the current state
        NightTime.SetActive(toggle);
        manager.ActiveMonster.IsSleepingStatus = toggle;
        SleepZs.SetActive(toggle);
        //updates the monsters degration when going to different scenes and back home.
        //does not degrate from opening the game or coming back from combat
        if (manager.PreviousSecene != 0 || manager.PreviousSecene != 1 || manager.PreviousSecene != 9)
            manager.ActiveMonster.AtGameWakeUp(saver.FindTimeDifference());

        NightTime.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        NightTime.transform.localPosition = new Vector3(-14, 4, -74);
        //adds the sleep toggle method to the button's on click trigger.
        thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(TaskOnClick);
    }



    private void OnDestroy()
    {/*
        try 
        {
            manager.SleepMemory = toggle;
        }
        catch { }
        saver.SaveTime()*/
    }


    //toggle sleep method.
    void TaskOnClick() 
    {
        //toggles the night shade.
        toggle = !toggle;
        NightTime.SetActive(toggle);
        SleepZs.SetActive(toggle);
        //updates the monster's sleep status.
        manager.ActiveMonster.IsSleepingStatus = toggle;
        //temporary UI feedback and debug log
        manager.ActiveMonster.DebugMonster();

        FindObjectOfType<SoundManager>().play("ButtonClick");

        foodInv.SetBool("Open", !foodInv.GetBool("Open"));
    }
}
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

    public Button[] Disabled;


    // Start is called before the first frame update
    void Start()
    {
        //finds the __app in order to reference the gamemanager.
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        if(manager.PreviousSecene == 11)
            toggle = manager.SleepMemory;
        //creates the shade and sets it to inactive, parents it under the canvas and transforms its position under the new parent.
        NightTime = Instantiate(NightTime);
        //sets the current state
        NightTime.SetActive(toggle);
        manager.ActiveMonster.IsSleepingStatus = toggle;
        SleepZs.SetActive(toggle);

        foreach (Button disable in Disabled)
        {
            disable.interactable = !toggle;
        }

        NightTime.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        NightTime.transform.localPosition = new Vector3(-14, 4, -74);
        //adds the sleep toggle method to the button's on click trigger.
        thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(TaskOnClick);
    }



    private void OnDestroy()
    {
        if (manager != null)
            manager.SleepMemory = toggle;
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

        if(toggle == true)
        {
            foodInv.SetBool("Open", false);
        }

        foreach (Button disable in Disabled)
        {
            disable.interactable = !toggle;
        }
    }
}
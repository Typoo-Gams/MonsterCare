using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSleep_Button : MonoBehaviour
{
    public GameObject NightTime;
    public GameManager manager;
    public Text zZz;
    Button thisButton;
    float counter;
    bool toggle;

    // Start is called before the first frame update
    void Start()
    {
        //finds the __app in order to reference the gamemanager.
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        //creates the shade and sets it to inactive, parents it under the canvas and transforms its position under the new parent.
        NightTime = Instantiate(NightTime);
        NightTime.SetActive(false);
        NightTime.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        NightTime.transform.localPosition = new Vector3(-14, 4, -74);
        //adds the sleep toggle method to the button's on click trigger.
        thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(TaskOnClick);
    }


    //the functionality in update should probably be moved to the monster's script itself


    //updates the monster's sleep value every second.
    private void Update()
    {
        counter += Time.deltaTime;
        if (counter > 1) 
            manager.ActiveMonster.UpdateSleeping(manager.ActiveMonster.IsSleepingStatus, 1);    
    }


    //toggle sleep method.
    void TaskOnClick() 
    {
        //toggles the night shade.
        toggle = !toggle;
        NightTime.SetActive(toggle);
        
        //updates the monster's sleep status.
        manager.ActiveMonster.IsSleepingStatus = !manager.ActiveMonster.IsSleepingStatus;
        //temporary UI feedback and debug log
        zZz.gameObject.SetActive(manager.ActiveMonster.IsSleepingStatus);
        manager.ActiveMonster.DebugMonster();
    }
}
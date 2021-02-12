using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSleep_Button : MonoBehaviour
{
    public Image NightTime;
    public GameManager manager;
    public Text zZz;
    Button thisButton;
    float counter;

    // Start is called before the first frame update
    void Start()
    {
        NightTime.enabled = false;
        thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(TaskOnClick);
    }

    private void Update()
    {
        counter += Time.deltaTime;
        if (counter > 1) 
            manager.ActiveMonster.UpdateSleeping(manager.ActiveMonster.IsSleepingStatus);    
    }

    void TaskOnClick() 
    {
        NightTime.enabled = !NightTime.enabled;

        manager.ActiveMonster.IsSleepingStatus = !manager.ActiveMonster.IsSleepingStatus;
        manager.ActiveMonster.DebugMonster();
        zZz.gameObject.SetActive(manager.ActiveMonster.IsSleepingStatus);
    }
}
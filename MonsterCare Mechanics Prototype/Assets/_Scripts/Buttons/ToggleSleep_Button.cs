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
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        NightTime = Instantiate(NightTime);
        NightTime.SetActive(false);
        NightTime.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        NightTime.transform.localPosition = new Vector3(-14, 4, -74);
        thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(TaskOnClick);
    }

    private void Update()
    {
        counter += Time.deltaTime;
        if (counter > 1) 
            manager.ActiveMonster.UpdateSleeping(manager.ActiveMonster.IsSleepingStatus, 1);    
    }

    void TaskOnClick() 
    {
        toggle = !toggle;
        NightTime.SetActive(toggle);
        

        manager.ActiveMonster.IsSleepingStatus = !manager.ActiveMonster.IsSleepingStatus;
        manager.ActiveMonster.DebugMonster();
        zZz.gameObject.SetActive(manager.ActiveMonster.IsSleepingStatus);
    }
}
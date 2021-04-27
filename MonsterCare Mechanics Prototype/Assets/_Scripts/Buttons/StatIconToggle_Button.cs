using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatIconToggle_Button : MonoBehaviour
{
    public Animator anim;
    GameManager manager;
    bool StateChanged;
    Animator ThisAnim;
    bool IsOpen;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        ThisAnim = GetComponent<Animator>();
        IsOpen = true;
    }

    private void Update()
    {
        if (manager.HideUI && !StateChanged)
        {
            StateChanged = true;
            anim.SetBool("Open", true);
        }
        if (!manager.HideUI && StateChanged)
        {
            StateChanged = false;
            anim.SetBool("Open", false);
        }

        if (manager.ActiveMonster.HealthStatus < 30 || manager.ActiveMonster.EnergyStatus < 4 || manager.ActiveMonster.HungerStatus < 30 || manager.ActiveMonster.SleepStatus < 30 || manager.ActiveMonster.HappinessStatus < 30)
        {
            if (!IsOpen) 
                ThisAnim.SetBool("LowStats", true);
            else
                ThisAnim.SetBool("LowStats", false);
        }
    }

    void TaskOnClick()
    {
        IsOpen = anim.GetBool("Open");

        anim.SetBool("Open", !IsOpen);
        FindObjectOfType<SoundManager>().play("ButtonClick");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeChanger_button : MonoBehaviour
{
    Button myButton;
    bool Speed = true;
    const float Slow = 0.1f, Fast = 0.0001f;

    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();

        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(TaskOnClick);
    }


    private void TaskOnClick()
    {
        Speed = !Speed;

        if (!Speed)
        {
            myButton.GetComponentInChildren<Text>().text = "Time: Fast";
            manager.MonsterUpdateSpeed = Fast;
        }
        else
        {
            myButton.GetComponentInChildren<Text>().text = "Time: Slow";
            manager.MonsterUpdateSpeed = Slow;
        }
    }
}

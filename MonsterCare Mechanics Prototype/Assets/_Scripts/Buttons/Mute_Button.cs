using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mute_Button : MonoBehaviour
{
    SoundManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<SoundManager>();
        gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);

        if (manager.SoundMuted)
        {
            gameObject.GetComponentInChildren<Text>().text = "Muted";

        }
        else
        {
            gameObject.GetComponentInChildren<Text>().text = "Mute";
        }
    }

    void TaskOnClick() 
    {
        manager.SetMute(!manager.SoundMuted);

        if (manager.SoundMuted)
        {
            gameObject.GetComponentInChildren<Text>().text = "Muted";

        }
        else
        {
            gameObject.GetComponentInChildren<Text>().text = "Mute";
        }
    }
}

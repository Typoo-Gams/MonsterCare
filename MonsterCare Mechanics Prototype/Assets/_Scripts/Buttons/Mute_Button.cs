using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mute_Button : MonoBehaviour
{
    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick() 
    {
        manager.SoundMuted = !manager.SoundMuted;
        if (Camera.main.GetComponent<AudioListener>() != null)
            Camera.main.GetComponent<AudioListener>().enabled = !manager.SoundMuted;
    }
}

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
    }

    void TaskOnClick() 
    {
        manager.SoundMuted = !manager.SoundMuted;

        if (manager.SoundMuted)
        {
            gameObject.GetComponentInChildren<Text>().text = "Muted";

            //mutes all audiosources
            foreach (AudioSource sounds in manager.gameObject.GetComponents<AudioSource>())
            {
                sounds.mute = true;
            }
        }
        else
        {
            gameObject.GetComponentInChildren<Text>().text = "Mute";

            //unmutes all audiosources
            foreach (AudioSource sounds in manager.gameObject.GetComponents<AudioSource>())
            {
                sounds.mute = false;
            }
        }
    }
}

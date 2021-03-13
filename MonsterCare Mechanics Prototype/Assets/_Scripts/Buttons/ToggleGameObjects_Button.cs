using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGameObjects_Button : MonoBehaviour
{
    public bool PlaySounds;

    bool IsActive;
    public bool rendererOrEnabled;
    public GameObject[] toggleThis;
    
    // Start is called before the first frame update
    void Start()
    {
        TaskOnClick();
        gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }
    
    void TaskOnClick() 
    {
        if (rendererOrEnabled)
        {
            for (int i = 0; i < toggleThis.Length; i++)
            {
                toggleThis[i].SetActive(IsActive);
                IsActive = !IsActive;

            }
        }
        else
        {
            for (int i = 0; i < toggleThis.Length; i++)
            {
                toggleThis[i].GetComponent<Renderer>().enabled = IsActive;
                IsActive = !IsActive;
            }
        }
        FindObjectOfType<SoundManager>().play("ButtonClick");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatIconToggle_Button : MonoBehaviour
{
    public bool PlaySounds;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        bool IsOpen = anim.GetBool("Open");

        anim.SetBool("Open", !IsOpen);
        FindObjectOfType<SoundManager>().play("ButtonClick");
    }
}

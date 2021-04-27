using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatIconToggle_Button : MonoBehaviour
{
    public Animator anim;
    GameManager manager;
    bool StateChanged;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
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
    }

    void TaskOnClick()
    {
        bool IsOpen = anim.GetBool("Open");

        anim.SetBool("Open", !IsOpen);
        FindObjectOfType<SoundManager>().play("ButtonClick");
    }
}

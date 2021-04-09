using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodInvToggle_Button : MonoBehaviour
{
    public bool PlaySounds;

    public Animator anim;

    GameManager manager;

    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
    }

    public void TaskOnClick()
    {
        if(manager.ActiveMonster.IsSleepingStatus == false)
        {
            button.enabled = true;
            anim.enabled = true;
            bool IsOpen = anim.GetBool("Open");

            anim.SetBool("Open", !IsOpen);
            FindObjectOfType<SoundManager>().play("ButtonClick");
        }
        /*else if (manager.ActiveMonster.IsSleepingStatus == true)
        {
            anim.enabled = false;
            button.enabled = false;
        }*/
    }
}

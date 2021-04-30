using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ExitPreload : MonoBehaviour
{
    public int scene;
    public Text input;
    GameManager manager;

    void Start()
    {
        if(GetComponent<Button>() != null)
            GetComponent<Button>().onClick.AddListener(TaskOnClick);
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
    }

    private void Update()
    {
        if (!manager.DevBuild)
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);

        if (Input.GetKeyDown(KeyCode.Return))
            TaskOnClick();
    }

    void TaskOnClick() 
    {
        string textinput = input.text;
        if (scene != 0) 
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
        }
        else 
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(textinput);
        }
    }
}
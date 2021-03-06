using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ExitPreload : MonoBehaviour
{
    public int scene;
    public Text input;
    public bool DevBuild;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    private void Update()
    {
        if (!DevBuild)
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

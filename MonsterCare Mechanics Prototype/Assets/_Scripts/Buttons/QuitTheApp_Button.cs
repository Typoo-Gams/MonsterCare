using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitTheApp_Button : MonoBehaviour
{
    public Canvas exitCanvas;
    public Button Cancel, Quit;

    private void Start()
    {
        Cancel.onClick.AddListener(CancelQuit);
        Quit.onClick.AddListener(ExitGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitCanvas.enabled = true;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CancelQuit()
    {
        exitCanvas.enabled = false;
    }
}

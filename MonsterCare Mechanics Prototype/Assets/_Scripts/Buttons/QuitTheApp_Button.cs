using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitTheApp_Button : MonoBehaviour
{
    public bool PlaySounds;

    public GameObject exitMenu;
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
            exitMenu.SetActive(true);
        }
    }

    public void ExitGame()
    {
        FindObjectOfType<SoundManager>().play("ButtonClick");
        Application.Quit();
    }

    public void CancelQuit()
    {
        FindObjectOfType<SoundManager>().play("ButtonClick");
        exitMenu.SetActive(false);
    }
}

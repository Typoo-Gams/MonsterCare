using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGame_MainMenu : MonoBehaviour
{
    public Text Play;
    public GameObject NewGameButton;

    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();

        if (manager.NewSave) 
        {
            Play.text = "TAP TO START A NEW GAME";
            NewGameButton.SetActive(false);
        }
    }
}

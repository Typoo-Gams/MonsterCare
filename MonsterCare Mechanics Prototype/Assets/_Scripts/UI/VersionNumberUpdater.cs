using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersionNumberUpdater : MonoBehaviour
{
    public Text text;

    void Start()
    {
        GameManager manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        GameSaver saver = new GameSaver();
        text.text = "Prototype Build V." + manager.GameVersion;
        if (manager.GameVersion != saver.LoadgameVersion() && saver.LoadgameVersion() != "") 
        {
            text.text += "\n\nDetected a different game version in save file.\nThis might cause unexpected issues.";
        }
    }
}
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
        text.text = "Prototype Build V." + manager.GameVersion;
    }
}
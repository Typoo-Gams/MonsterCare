using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterLoader : MonoBehaviour
{
    GameSaver Saver = new GameSaver();
    public ClearSave clear;
    public GameObject Manager;
    GameManager manager;
    GameObject SavedMonster;
    public Text pathText;

    // Start is called before the first frame update
    void Start()
    {
        manager = Manager.GetComponent<GameManager>();
        string path = Saver.GetMonsterPrefab();
        Debug.Log(Saver.LoadgameVersion() + " ?= " + manager.GameVersion + " " + this);
        if (manager.GameVersion != Saver.LoadgameVersion()) 
        {
            clear.ResetSave();
            path = Saver.GetMonsterPrefab();
            Debug.Log("Detected a different game version");
        }
        if (path == "None")
        {
            SavedMonster = Resources.Load<GameObject>("Prefabs/MonsterStuff/Monsters/DefaultStartingMonster");
        }
        if (path != "None")
        {
            SavedMonster = Resources.Load<GameObject>(path);
        }
        Instantiate(SavedMonster, SavedMonster.transform.position, Quaternion.identity, Manager.transform);
    }

    private void Update()
    {
        pathText.text = "Path: " + Saver.GetMonsterPrefab() + "\nLastGameVersion: " + Saver.LoadgameVersion() + "\nCurrentGameVersion: " + manager.GameVersion;
    }
}
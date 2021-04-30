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
    void Awake()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        //Finds the prefab path.
        string path = Saver.GetMonsterPrefab();
        Debug.Log("Loaded Path: " + path);

        //if the game version doesnt match the current one, wipe the save
        if (Saver.LoadgameVersion() == "")
        {
            Debug.Log("Detected first time startup");
            manager.NewSave = true;
            clear.ResetSave();
            //resets the path string
            path = Saver.GetMonsterPrefab();
        }
        //if the save doesnt have any previously saved monster then load the starting monster (this happens with new saves/first time boot)
        if (path == "None" || path == "" ||manager.NewSave)
        {
            SavedMonster = Resources.Load<GameObject>("Prefabs/MonsterStuff/Monsters/Gen 0/Child_Gen0");
            Debug.Log("Loading Start monster");
        }
        //if there is a saved monster then load it.
        else
        {
            SavedMonster = Resources.Load<GameObject>(path);
            Debug.Log("loaded Monster");
        }
        //create the monster that was loaded.
        if (SavedMonster != null) 
        {
            GameObject SpawnedMonster = Instantiate(SavedMonster, SavedMonster.transform.position, Quaternion.identity);
            SpawnedMonster.transform.SetParent(Manager.transform, false);
        }
    }

    private void Update()
    {
        //updates the bebugging text in the preload scene
        pathText.text = "Path: " + Saver.GetMonsterPrefab() + "\nLastGameVersion: " + Saver.LoadgameVersion() + "\nCurrentGameVersion: " + manager.GameVersion;
    }
}
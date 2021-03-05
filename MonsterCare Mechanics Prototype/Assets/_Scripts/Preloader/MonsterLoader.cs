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
        //Finds the prefab path.
        string path = Saver.GetMonsterPrefab();
        //if the game version doesnt match the current one, wipe the save
        if (manager.GameVersion != Saver.LoadgameVersion()) 
        {
            clear.ResetSave();
            //resets the path string
            path = Saver.GetMonsterPrefab();
            Debug.Log("Detected a different game version");
        }
        //if the save doesnt have any previously saved monster then load the starting monster (this happens with new saves/first time boot)
        if (path == "None")
        {
            SavedMonster = Resources.Load<GameObject>("Prefabs/MonsterStuff/Monsters/DefaultStartingMonster");
        }
        //if there is a saved monster then load it.
        if (path != "None")
        {
            SavedMonster = Resources.Load<GameObject>(path);
        }
        //create the monster that was loaded.
        Instantiate(SavedMonster, SavedMonster.transform.position, Quaternion.identity, Manager.transform);
    }

    private void Update()
    {
        //updates the bebugging text in the preload scene
        pathText.text = "Path: " + Saver.GetMonsterPrefab() + "\nLastGameVersion: " + Saver.LoadgameVersion() + "\nCurrentGameVersion: " + manager.GameVersion;
    }
}
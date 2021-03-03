using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterLoader : MonoBehaviour
{
    GameSaver Saver = new GameSaver();
    public GameObject Manager;
    GameManager manager;
    GameObject SavedMonster;
    public Text pathText;

    // Start is called before the first frame update
    void Start()
    {
        manager = Manager.GetComponent<GameManager>();
        string path = Saver.GetMonsterPrefab();

        if (manager.GameVersion != Saver.LoadgameVersion()) 
        {
            Saver.WipeSave();
        }
        if (path == "None")
        {
            SavedMonster = Resources.Load<GameObject>("Prefabs/MonsterStuff/Monsters/DefaultStartingMonster");
        }
        else
        {
            SavedMonster = Resources.Load<GameObject>(path);
        }
        GameObject spawn = Instantiate(SavedMonster, SavedMonster.transform.position, Quaternion.identity, Manager.transform);
        //spawn.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Update()
    {
        pathText.text = "Path: " + Saver.GetMonsterPrefab() + "\nLastGameVersion: " + Saver.LoadgameVersion() + "\nCurrentGameVersion: " + manager.GameVersion;
    }
}

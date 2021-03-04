using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearSave : MonoBehaviour
{
    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        gameObject.GetComponent<Button>().onClick.AddListener(ResetSave);
    }

    public void ResetSave() 
    {
        try
        {
            Destroy(manager.MonsterObject);
            manager.ActiveMonster = null;
            manager.MonsterObject = null;
        }
        catch
        {
            Debug.Log("No Monster was spawned yet to wipe from save.");
        }
        GameSaver Saver = new GameSaver();
        Saver.WipeSave();
    }

    private void Update()
    {
        if (manager.ActiveMonster == null) 
        {
            Application.Quit();
            Debug.LogWarning("exiting");
        }
    }
}

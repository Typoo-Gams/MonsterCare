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
        gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick() 
    {
        Destroy(manager.MonsterObject);
        GameSaver Saver = new GameSaver();
        Saver.WipeSave();
        manager.ActiveMonster = null;
        manager.MonsterObject = null;
        Debug.Log("SaveFiles was wiped.");
    }
}

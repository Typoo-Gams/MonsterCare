using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportsController : MonoBehaviour
{
    GameSaver saver;
    GameManager manager;
    public GameObject Monster;

    public void Start()
    {
        saver = new GameSaver();
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
    }

    private void OnMouseDown()
    {
        saver.SaveObtainedMonster(Monster.name, true);
        Destroy(gameObject);
        manager.HideUI = false;        
    }

    private void OnDestroy()
    {
        saver.SaveObtainedMonster(Monster.name, true);
        manager.HideUI = false;
    }
}

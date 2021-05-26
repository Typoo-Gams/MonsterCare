using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportsController : MonoBehaviour
{
    GameSaver saver;
    GameManager manager;
    public GameObject Monster;
    public bool KeepHiddenUI;
    public GameObject tutorial;

    public void Start()
    {
        saver = new GameSaver();
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
    }

    private void OnMouseDown()
    {
        saver.SaveObtainedMonster(Monster.name, true);
        Destroy(gameObject);
        if (!KeepHiddenUI)
        {
            manager.HideUI = false;
        }
        else
        {
            saver.IsTutorialDone(1);
            Instantiate(tutorial).transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        }
    }

    private void OnDestroy()
    {
        saver.SaveObtainedMonster(Monster.name, true);
        if (!KeepHiddenUI)
        {
            manager.HideUI = false;
        }
            saver.IsTutorialDone(1);
    }
}

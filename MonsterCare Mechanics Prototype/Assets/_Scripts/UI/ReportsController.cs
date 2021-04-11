using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportsController : MonoBehaviour
{
    GameSaver saver;
    public GameObject Monster;

    public void Start()
    {
        saver = new GameSaver();
    }

    private void OnMouseDown()
    {
        saver.SaveObtainedMonster(Monster.name, true);
        Destroy(gameObject);
    }
}

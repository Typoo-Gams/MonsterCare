using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySliderTransform : MonoBehaviour
{
    string[] enemyMonsterNames = { "FireEnemyPrefab",   //0
                                   "FireEnemy2Prefab",  //1
                                   "EarthEnemyPrefab",  //2
                                   "AirEnemyPrefab",    //3
                                   "WaterEnemyPrefab"}; //4

    Slider TransformThis;
    public string path;

    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();

        TransformThis = manager.EnemyMonster.GetHealthbar();

        string name = "";
        for (int i = 0; i < enemyMonsterNames.Length-1; i++)
        {
            if (path.Contains(enemyMonsterNames[i]))
            {
                name = enemyMonsterNames[i];
                
            }
        }
        switch (name)
        {
            case "AirEnemyPrefab":
                TransformThis.transform.localPosition = new Vector3(500f, 115f, 0f);
                break;

            case "EarthEnemyPrefab":
                TransformThis.transform.localPosition = new Vector3(0f, 188f, 0f);
                break;
        }
        Debug.Log(name + "  " + path);

    }

    public void SetHealthbar(Slider healthbar)
    {
        TransformThis = healthbar;
    }

    public void SetPath(string paths)
    {
        path = paths;
    }
}

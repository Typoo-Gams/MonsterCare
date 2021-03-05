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
    string path;

    // Start is called before the first frame update
    void Start()
    {
        string name = "";
        for (int i = 0; i < enemyMonsterNames.Length; i++)
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
        }
        Debug.Log(name + "  " + path);

    }

    /*public void SetHealthbar(Slider healthbar)
    {
        TransformThis = healthbar;
    }

    public void SetPath(string paths)
    {
        path = paths;
    }*/
}

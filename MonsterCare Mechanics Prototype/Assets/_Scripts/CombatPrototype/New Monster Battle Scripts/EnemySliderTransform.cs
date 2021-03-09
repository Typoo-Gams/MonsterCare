using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySliderTransform : MonoBehaviour
{
    /* "FireEnemyPrefab",   //0
       "FireEnemy2Prefab",  //1
       "EarthEnemyPrefab",  //2
       "AirEnemyPrefab",    //3
       "WaterEnemyPrefab"}; */

    Slider TransformThis;
    public string path;
    public Vector3 HealthbarPosition;
    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();

        TransformThis = manager.EnemyMonster.GetHealthbar();
        TransformThis.transform.localPosition = HealthbarPosition;
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

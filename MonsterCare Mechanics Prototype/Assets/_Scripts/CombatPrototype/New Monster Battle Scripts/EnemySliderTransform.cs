using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySliderTransform : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("HealthSlider1(Clone)");
        Debug.Log("slider found");

        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Touch_Buttons_Food : MonoBehaviour
{
    public GameObject foodPrefab;

    public void OnClickedNow()
    {
        Instantiate(foodPrefab);
    }
    
}

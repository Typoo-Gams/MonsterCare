using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unused_Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick() 
    {
        Debug.LogError("This button is unused.");
    }
}

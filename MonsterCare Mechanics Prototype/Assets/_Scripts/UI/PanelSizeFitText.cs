using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSizeFitText : MonoBehaviour
{
    Text Child;
    RectTransform thisTransform;

    // Start is called before the first frame update
    void Start()
    {
        Child = GetComponentInChildren<Text>();
        thisTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}

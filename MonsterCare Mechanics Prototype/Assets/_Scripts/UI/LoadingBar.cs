using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Image LoadBarFill;
    public float CurrentWhaitTime, whaitTime;

    // Update is called once per frame
    void Update()
    {
        float index = CurrentWhaitTime / whaitTime;
        LoadBarFill.fillAmount = index;
    }
}

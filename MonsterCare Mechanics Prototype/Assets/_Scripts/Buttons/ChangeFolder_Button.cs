using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFolder_Button : MonoBehaviour
{
    bool isActive;
    Image page;

    public void TaskOnClick()
    {
        if (!isActive)
        {
            page.enabled = true;
            isActive = true;
            Debug.LogWarning("enabled page");
        }

        if (isActive)
        {
            page.enabled = false;
            isActive = false;
            Debug.LogWarning("disabled page");
        }
        
    }
}

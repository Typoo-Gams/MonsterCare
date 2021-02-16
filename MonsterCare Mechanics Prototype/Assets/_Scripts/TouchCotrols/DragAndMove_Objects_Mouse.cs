using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndMove_Objects_Mouse : MonoBehaviour
{
    bool clicked = false;

    private void Update()
    {
        if (clicked)
        {
            Vector3 direction = Input.mousePosition;
            Vector3 screenPos = Camera.main.ScreenToWorldPoint(direction);
            screenPos.z = 0;
            transform.position = screenPos;
        }
    }


    private void OnMouseDown()
    {
        clicked = true;
    }

    private void OnMouseUp()
    {
        clicked = false;
    }
}

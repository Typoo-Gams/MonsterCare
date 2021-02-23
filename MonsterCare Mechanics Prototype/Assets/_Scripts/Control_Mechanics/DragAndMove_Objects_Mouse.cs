using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndMove_Objects_Mouse : MonoBehaviour
{
    bool clicked = false;

    private void Update()
    {
        //If the gameobject is being touched move the object to the mouse position.
        if (clicked)
        {
            Vector3 direction = Input.mousePosition;
            Vector3 screenPos = Camera.main.ScreenToWorldPoint(direction);
            screenPos.z = 0;
            transform.position = screenPos;
        }
    }


    //When this gameobject is beig touched set to true.
    private void OnMouseDown()
    {
        clicked = true;
    }


    //When this gameobject is being let go off set to false.
    private void OnMouseUp()
    {
        clicked = false;
    }
}

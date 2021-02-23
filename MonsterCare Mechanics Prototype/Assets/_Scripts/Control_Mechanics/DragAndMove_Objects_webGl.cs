using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndMove_Objects_webGl : MonoBehaviour
{
    bool clicked = false;

    private void Update()
    {
        Debug.Log(Input.mousePosition);

        //If the gameobject is being touched move the object to the mouse position.
        if (clicked)
        {
            Vector3 direction = Input.mousePosition;
            direction.z = 0;
            transform.position = direction;
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

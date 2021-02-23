using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndMove_Object : MonoBehaviour
{
    public Vector3 direction;
    public Vector3 screenPos;
    //bool checking if the gameobject is being touched.
    public bool touched;

    // Update is called once per frame
    void Update()
    {
        if (touched)
        {
            //get the touch and move the gameobject to its position
            Touch touch = Input.GetTouch(0);
            direction = touch.position;
            screenPos = Camera.main.ScreenToWorldPoint(direction);
            screenPos.z = 0;
            transform.position = screenPos;
        }
    }


    //When this gameobject is touched set to true
    private void OnMouseDown()
    {
        touched = true;
    }


    //When this gameobject is let go of set to false
    private void OnMouseUp()
    {
        touched = false;
    }
}
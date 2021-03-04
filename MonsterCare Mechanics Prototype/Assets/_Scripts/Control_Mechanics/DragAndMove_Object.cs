using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndMove_Object : MonoBehaviour
{
    public Vector3 direction;
    public Vector3 screenPos;

    private void OnMouseDrag()
    {
        direction = Input.mousePosition;
        screenPos = Camera.main.ScreenToWorldPoint(direction);
        screenPos.z = transform.position.z;
        transform.position = screenPos;
    }

}
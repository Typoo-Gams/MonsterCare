using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndMove_Objects_Mouse : MonoBehaviour
{
    private void OnMouseDown()
    {
        Vector3 direction =  Input.mousePosition;
        transform.position = Camera.main.ScreenToWorldPoint(direction);
    }
}

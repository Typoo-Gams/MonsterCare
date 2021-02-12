using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndMove_Objects_Mouse : MonoBehaviour
{
    private void OnMouseDown()
    {
        transform.position = Input.mousePosition;
    }
}

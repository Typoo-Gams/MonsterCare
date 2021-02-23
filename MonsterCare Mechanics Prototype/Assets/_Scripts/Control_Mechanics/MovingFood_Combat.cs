using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFood_Combat : MonoBehaviour
{

    bool canDrag = false;

    Vector3 itemPos;

    void Update()
    {
        transform.position = Input.mousePosition;
        if (!Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            canDrag = false;
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            if (Input.GetMouseButton(0) || Input.touchCount > 0)
            {
                canDrag = true;
                other.transform.position = Input.mousePosition;
            }

        }
    }
}


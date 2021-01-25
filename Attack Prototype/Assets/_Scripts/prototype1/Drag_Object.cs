using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag_Object : MonoBehaviour
{
    bool isMoving;
    bool drag;
    Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        isMoving = false;
        drag = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) || Input.touchCount == 1)
        {
            if (coll == Physics2D.OverlapPoint(mousePos))
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

            if (isMoving)
            {
                drag = true;
            }
        }

        if (drag)
        {
            transform.position = mousePos;
        }
        if (Input.GetMouseButtonUp(0) || Input.touchCount <= 0)
        {
            isMoving = false;
            drag = false;
        }
    }
}
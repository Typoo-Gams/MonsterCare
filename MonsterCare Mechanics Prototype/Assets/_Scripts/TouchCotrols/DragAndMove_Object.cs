using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndMove_Object : MonoBehaviour
{
    public Vector2 startPos;
    public bool directionChosen;
    public Vector3 direction;
    private Camera cam;
    public Vector3 screenPos;
    public bool touched;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (touched)
        {

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // Handle finger movements based on touch phase.
                switch (touch.phase)
                {
                    // Record initial touch position.
                    case TouchPhase.Began:
                        startPos = touch.position;
                        directionChosen = false;
                        break;

                    // Determine direction by comparing the current touch position with the initial one.
                    case TouchPhase.Moved:
                        direction = touch.position;
                        screenPos = Camera.main.ScreenToWorldPoint(direction);
                        screenPos.z = 0;
                        transform.position = screenPos;
                        break;

                    // Report that a direction has been chosen when the finger is lifted.
                    case TouchPhase.Ended:
                        directionChosen = true;
                        touched = false;
                        break;
                }
            }
        }
    }
    private void OnMouseDown()
    {
        touched = true;
    }
}
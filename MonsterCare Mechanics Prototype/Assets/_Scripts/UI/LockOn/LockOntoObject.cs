using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOntoObject : MonoBehaviour
{
    GameObject[] Targets;
    bool Locked;
    // Start is called before the first frame update
    void Start()
    {
        Targets = GameObject.FindGameObjectsWithTag("LockOn");
    }

    private void OnMouseDown()
    {
        Locked = false;
        
    }

    private void OnMouseUp()
    {
        if (transform.parent != null)
            if (transform.parent.parent != null)
                transform.SetParent(transform.parent.parent);

        Locked = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "LockOn" && Locked)
        {
            transform.SetParent(collision.gameObject.transform, true);
            transform.localPosition = new Vector3(0, 0, 0);
            collision.collider.enabled = false;
        }
    }
}

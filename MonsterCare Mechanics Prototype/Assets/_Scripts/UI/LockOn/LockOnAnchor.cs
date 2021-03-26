using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnAnchor : MonoBehaviour
{
    Collider2D collider;
    int childCount;
    private void Start()
    {
        collider = GetComponent<Collider2D>();
        childCount = transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.childCount == childCount) 
        {
            collider.enabled = true;
        }
    }
}

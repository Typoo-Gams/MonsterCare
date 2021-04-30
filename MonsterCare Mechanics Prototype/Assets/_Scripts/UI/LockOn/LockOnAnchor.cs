using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnAnchor : MonoBehaviour
{
    new Collider2D collider;
    public GameObject LockedObject;
    public Vector3 lockedPosition;
    public int ChildCount;
    

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        /* didnt work, needs to run when the child has been attached.
        if (transform.GetComponentInChildren<LockOntoObject>() != null)
            LockedObject = transform.GetComponentInChildren<LockOntoObject>().gameObject;
        */
    }

    // Update is called once per frame
    void Update()
    {
        //Reset this anchor if there is no children.
        if (transform.childCount == 0)
        {
            LockedObject = null;
        }
        //When a locked object is set and its a child of the anchor then snap it into position.
        if (LockedObject != null) 
        {
            foreach (LockOntoObject child in transform.GetComponentsInChildren<LockOntoObject>())
            {
                if (child.name == LockedObject.name)
                {
                    LockedObject.transform.localPosition = lockedPosition;
                    LockedObject.transform.localRotation = Quaternion.identity;
                    //Locking Checking for food item
                    if (LockedObject.GetComponent<FoodManager_FoodObject>() != null)
                        LockedObject.GetComponent<FoodManager_FoodObject>().IsInInventory = true;
                }
            }
        }
        else
        {
            //enable the collider when no locked object is chosen.
            collider.enabled = true;
        }   
    }
}

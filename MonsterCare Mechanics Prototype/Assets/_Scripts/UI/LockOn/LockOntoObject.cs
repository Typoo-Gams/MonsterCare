using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOntoObject : MonoBehaviour
{
    public GameObject[] Targets;
    public bool Locked;
    //parent to this object when this object is not on an anchor.
    public GameObject UnlockedParent;

    private void Start()
    {
        UnlockedParent = GameObject.Find("Canvas_Home");
    }

    private void OnMouseDown()
    {
        //Detaches this object from the anchor when its being held by a player.
        Locked = false;
        if (transform.GetComponentInParent<LockOnAnchor>() != null)
        {
            transform.GetComponentInParent<LockOnAnchor>().LockedObject = null;
            transform.SetParent(UnlockedParent.transform, true);
        }
    }

    private void OnMouseUp()
    {
        Locked = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //When this object's collider is on/inside the anchor and isnt being held by a player, lock it on.
        if (collision.GetComponent<LockOnAnchor>() != null) 
        {
            if (collision.GetComponent<LockOnAnchor>().LockedObject == null && Locked)
            {
                collision.GetComponent<LockOnAnchor>().LockedObject = gameObject;
                transform.SetParent(collision.gameObject.transform, true);
                collision.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}

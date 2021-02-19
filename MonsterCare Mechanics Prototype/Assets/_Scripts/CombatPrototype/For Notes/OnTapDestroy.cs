using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTapDestroy : MonoBehaviour
{
    public void OnMouseDown()
    {
        Destroy(gameObject);
    }
}

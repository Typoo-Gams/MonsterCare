using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    private void OnMouseUp()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    public FolderViewer_Button viewer;



    private void OnMouseUp()
    {
        Destroy(gameObject);
        viewer.isFilled = false;
    }
}

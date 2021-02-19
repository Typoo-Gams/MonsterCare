using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTapDestroy : MonoBehaviour
{
    public FoodTap_SceenLoader loadScene;
    
    bool isDestroyed;

    public void OnMouseDown()
    {
        Destroy(gameObject);
        isDestroyed = true;
    }


    private void OnDestroy()
    {
        if(isDestroyed == true)
        {

        }
    }
}

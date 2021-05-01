using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousLoadedScene : MonoBehaviour
{
    //updates the previous loaded scene
    //this script should be on every camera
    private void OnDestroy()
    {
        try
        {
            if (GameObject.Find("__app").GetComponentInChildren<GameManager>() != null)
            {
                GameManager Manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
                Manager.PreviousSecene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            }
        }
        catch 
        {
            //Debug.LogWarning("__app was not found");
        }
    }
}

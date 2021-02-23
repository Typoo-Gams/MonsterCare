using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevPreload : MonoBehaviour
{
    //This should happen first in any scene
    private void Awake()
    {
        //If the __app doesnt exist then load the preload scene.
        GameObject check = GameObject.Find("__app");
        if (check == null)
        { UnityEngine.SceneManagement.SceneManager.LoadScene("__preload"); Debug.Log("Loading __app"); }
    }
}

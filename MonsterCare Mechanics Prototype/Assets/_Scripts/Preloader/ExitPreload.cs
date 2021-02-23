using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPreload : MonoBehaviour
{
    public int scene;
    void Start()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
}

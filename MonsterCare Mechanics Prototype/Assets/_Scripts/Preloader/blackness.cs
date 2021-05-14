using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackness : MonoBehaviour
{
    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        if (manager.DevBuild)
        {
            gameObject.SetActive(false);
        }
    }
}

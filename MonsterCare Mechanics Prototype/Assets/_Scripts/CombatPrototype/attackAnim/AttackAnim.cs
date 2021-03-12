using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnim : MonoBehaviour
{
    public GameObject AttackAnime;
    public GameObject canvas;
    GameManager manager;

    
    void Start() 
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if (manager.Enemy == null || !manager.Enemy.active)
        {
            this.enabled = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject spawn = Instantiate(AttackAnime);
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 87.53f;
            pos.x += -0.52f;
            spawn.transform.localPosition = pos;
        }
    }
}
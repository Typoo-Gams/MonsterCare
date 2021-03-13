using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoodTap_SceenLoader : MonoBehaviour
{
    GameManager manager;
    public int spriteIndex;

    void Start() 
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        foreach(Food missingSprite in manager.FoodInventory) 
        {
            if (missingSprite.Sprite == -1) 
            {
                missingSprite.Sprite = spriteIndex;
            }
        }
    }

    public void OnMouseDown()
    {
        if (GameObject.FindGameObjectWithTag("Note") == null) 
        {
            Destroy(gameObject);
            SceneManager.LoadScene("LoadScene");
        }
        Debug.Log("Food Pickup");
    }
}

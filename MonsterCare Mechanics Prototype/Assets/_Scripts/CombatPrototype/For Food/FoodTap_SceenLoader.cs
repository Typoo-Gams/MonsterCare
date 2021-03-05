using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoodTap_SceenLoader : MonoBehaviour
{
    public void OnMouseDown()
    {
        if (GameObject.FindGameObjectWithTag("Note") == null) 
        {
            Destroy(gameObject);
            SceneManager.LoadScene("LoadScene");
        }
    }
}

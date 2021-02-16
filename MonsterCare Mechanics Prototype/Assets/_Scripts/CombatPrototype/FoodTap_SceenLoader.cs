using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoodTap_SceenLoader : MonoBehaviour
{
    private void WaitForPick()
    {
        //SceneManager.LoadScene(9);
        Debug.Log("hello");
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(9);
        //Invoke("WaitForPick", 1);
    }
}

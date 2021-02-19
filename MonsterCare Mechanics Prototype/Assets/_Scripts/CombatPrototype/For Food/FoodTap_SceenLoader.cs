using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoodTap_SceenLoader : MonoBehaviour
{

    private void OnMouseDown()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(9);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToHome_Button : MonoBehaviour
{

    public void OnClick()
    {
        SceneManager.LoadScene(1);
    }
}

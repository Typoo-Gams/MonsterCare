using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(8);
    }

    private void Update()
    {
        StartCoroutine(Waiting());
    }
}

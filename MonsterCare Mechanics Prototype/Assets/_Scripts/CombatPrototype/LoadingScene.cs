using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{

    //whait for 1 second then load the map
    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Map");
    }

    //when the loading screen is loaded call the coroutine.
    private void Start()
    {
        StartCoroutine(Waiting());
    }
}

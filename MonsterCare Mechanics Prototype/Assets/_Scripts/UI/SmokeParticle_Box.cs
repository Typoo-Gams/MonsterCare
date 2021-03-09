using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SmokeParticle_Box : MonoBehaviour
{
    public GameObject smokeParticles;
    public int SceneNumber = 0;
    public string SceneName = "";
    private float timer = 3;
    bool timerEnabled = false;

    private void Update()
    {
        if (timerEnabled)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (SceneName == "")
                {
                    SceneManager.LoadScene(SceneNumber);
                }
                else
                {

                    SceneManager.LoadScene(SceneName);
                }
            }
        }
    }

    private void OnMouseDown()
    {
        GameObject smokeParticlesPrefab = Instantiate(smokeParticles);
        smokeParticlesPrefab.transform.position = gameObject.transform.position;
        smokeParticlesPrefab.transform.SetParent(gameObject.transform, true);
        timerEnabled = true;
    }
}

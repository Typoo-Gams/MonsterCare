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

    int tapCounter;

    private void Update()
    {
        if (tapCounter > 2)
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

    private void OnMouseDown()
    {
        Smoke();
        tapCounter++;
    }

    private void Smoke()
    {
        GameObject smokeParticlesPrefab = Instantiate(smokeParticles);
        smokeParticlesPrefab.transform.position = gameObject.transform.position;
        smokeParticlesPrefab.transform.SetParent(gameObject.transform, true);
    }
}

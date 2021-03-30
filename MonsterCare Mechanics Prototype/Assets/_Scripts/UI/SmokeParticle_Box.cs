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
    GameManager manager;

    int tapCounter;

    private void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
    }

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

                SceneManager.LoadScene("LoadScene");
                manager.sceneName = "MonsterHome";
            }
        }
    }

    private void OnMouseDown()
    {
        FindObjectOfType<SoundManager>().play("BoxTapp");
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

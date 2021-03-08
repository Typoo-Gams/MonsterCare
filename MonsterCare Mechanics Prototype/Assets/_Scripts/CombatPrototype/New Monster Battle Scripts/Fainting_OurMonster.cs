using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fainting_OurMonster : MonoBehaviour
{
    GameManager manager;
    public GameObject black;

    public Text text;
    public GameObject[] UI;

    private void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Fainting();
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        black.SetActive(true);
        text.gameObject.SetActive(true);
        manager.ActiveMonster.GetHealthbar().gameObject.SetActive(false);
        manager.EnemyMonster.GetHealthbar().gameObject.SetActive(false);
        UI[0].SetActive(false);
        UI[1].SetActive(false);
        if (Input.GetMouseButtonDown(0))
        {
            text.gameObject.SetActive(false);
            SceneManager.LoadScene("MonsterHome");
        }
        
    }

    public void Fainting()
    {
        if(manager.ActiveMonster.DeathStatus == true)
        {
            StartCoroutine(Wait());
        }
    }
}

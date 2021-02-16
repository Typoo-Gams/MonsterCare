using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterFoodDrop : MonoBehaviour
{

    public MonsterManager_AttackPrototype KillThisMonster;
    public GameObject foodPrefab;
    float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = KillThisMonster.StartMonster.HealthStatus;
    }

    private IEnumerator WaitForPick()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(9);
    }


    private void Update()
    {
        FoodDrop();
        //Tapped();
        
    }

    private void FoodDrop()
    {
        if (KillThisMonster.StartMonster.DeathStatus)
        {
            GameObject objectToAppear = GameObject.FindGameObjectWithTag("MonsterFoodDrop");
            objectToAppear.GetComponent<Renderer>().enabled = true;

            GameObject objectToHide = GameObject.FindGameObjectWithTag("Monster");
            objectToHide.GetComponent<Renderer>().enabled = false;
        }
    }

    /*private void Tapped()
    {
        if ()
        {
            Destroy(GameObject.FindGameObjectWithTag("MonsterFoodDrop"));
            StartCoroutine(WaitForPick());
        }
    }*/
}

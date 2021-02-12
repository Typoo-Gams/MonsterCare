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
    }

    private void FoodDrop()
    {
        if (KillThisMonster.StartMonster.DeathStatus)
        {
            GameObject objectToAppear = GameObject.FindGameObjectWithTag("MonsterFoodDrop");
            objectToAppear.GetComponent<Renderer>().enabled = true;
        }
    }

    

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject objectToHide = GameObject.Find("Sprite_Chicken Variant");
        objectToHide.GetComponent<Renderer>().enabled = false;
    }

}

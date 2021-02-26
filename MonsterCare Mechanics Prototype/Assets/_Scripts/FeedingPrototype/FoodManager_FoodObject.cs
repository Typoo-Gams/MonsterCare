using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FoodManager_FoodObject : MonoBehaviour
{
    public int FoodPower = 10;
    public Text UI;
    Canvas CurrentCanvas;
    public List<Sprite> FoodSprites;
    GameManager manager;
    SpawnItem_Button SpawningButton;

    // Start is called before the first frame update
    void Start()
    {
        //Spawn with random food sprite
        int random = Random.Range(0, FoodSprites.Count);
        gameObject.GetComponent<SpriteRenderer>().sprite = FoodSprites[random];

        //This can be moved to the spawn object button
        CurrentCanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        gameObject.transform.localScale = new Vector3(13, 13, 13);
        gameObject.transform.localPosition = new Vector3(-118, 204, 0);

        //finds the __app for referencing the gamemanager. finds the button that spawns food.
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        SpawningButton = GameObject.FindGameObjectWithTag("FoodButton").GetComponent<SpawnItem_Button>();
        //toggles ability to spawn.
        SpawningButton.ToggleCanSpawn();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Monster")) 
        {
            //updates the monsters hunger when it eats the food
            manager.ActiveMonster.UpdateHunger(manager.ActiveMonster.HungerStatus + FoodPower);

            //spawns temporary feedback UI
            Text spawn = Instantiate(UI);
            spawn.text = "+" + FoodPower;
            spawn.transform.SetParent(CurrentCanvas.transform, false);
            spawn.transform.localPosition = new Vector3(347f, 276f, -19439.998f);
            spawn.transform.localScale = new Vector3(1,1,1);
            
            //destroys the food
            Destroy(gameObject);
            //plays the monsters eating animation.
            //temporary
            if (manager.MonsterObject.GetComponent<DefaultStarting_MonsterController>() != null)
                manager.MonsterObject.GetComponent<Animator>().Play("Child_Eating");  //needs generic reference
        }
    }

    //when the food is destroyed toggle the ability to spawn.
    private void OnDestroy()
    {
        SpawningButton.ToggleCanSpawn();
    }
}

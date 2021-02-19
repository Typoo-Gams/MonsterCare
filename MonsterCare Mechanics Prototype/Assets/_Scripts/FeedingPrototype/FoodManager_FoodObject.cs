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

    GameObject thisMonster;

    // Start is called before the first frame update
    void Start()
    {
        int random = Random.Range(0, FoodSprites.Count);
        gameObject.GetComponent<SpriteRenderer>().sprite = FoodSprites[random];
        CurrentCanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        gameObject.transform.localScale = new Vector3(75, 75, 75);
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        thisMonster = GameObject.FindGameObjectWithTag("Monster");
        SpawningButton = GameObject.FindGameObjectWithTag("FoodButton").GetComponent<SpawnItem_Button>();
        SpawningButton.ToggleCanSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Monster")) 
        {
            manager.ActiveMonster.UpdateHunger(manager.ActiveMonster.HungerStatus + FoodPower);
            Text spawn = Instantiate(UI);
            spawn.text = "+" + FoodPower;
            spawn.transform.SetParent(CurrentCanvas.transform, false);
            spawn.transform.localPosition = new Vector3(347f, 276f, -19439.998f);
            spawn.transform.localScale = new Vector3(1,1,1);
            
            Destroy(gameObject);
            thisMonster.GetComponent<Animator>().Play("Child_Eating");  //needs generic reference
            SpawningButton.ToggleCanSpawn();
        }
    }
}

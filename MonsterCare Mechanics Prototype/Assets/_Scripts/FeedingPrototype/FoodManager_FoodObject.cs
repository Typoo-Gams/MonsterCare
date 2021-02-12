using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FoodManager_FoodObject : MonoBehaviour
{
    DefaultStarting_MonsterController FeedThisMonster;
    public int FoodPower = 10;
    public Text UI;
    Canvas CurrentCanvas;
    public List<Sprite> FoodSprites;

    // Start is called before the first frame update
    void Start()
    {
        Monster temp = GameObject.FindGameObjectWithTag("Monster").GetComponent<DefaultStarting_MonsterController>().monster;
        int random = Random.Range(0, FoodSprites.Count);
        gameObject.GetComponent<SpriteRenderer>().sprite = FoodSprites[random];
        CurrentCanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        FeedThisMonster = GameObject.FindGameObjectWithTag("Monster").GetComponent<DefaultStarting_MonsterController>();
        gameObject.transform.localScale = new Vector3(75, 75, 75);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Monster")) 
        {
            FeedThisMonster.monster.UpdateHunger(FeedThisMonster.monster.HungerStatus + 1);
            Text spawn = Instantiate(UI);
            spawn.transform.SetParent(CurrentCanvas.transform, false);
            spawn.transform.localPosition = new Vector3(347f, 276f, -19439.998f);
            spawn.transform.localScale = new Vector3(1,1,1);
            
            Destroy(gameObject);
        }
    }
}

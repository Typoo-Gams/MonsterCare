using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FoodManager_FoodObject : MonoBehaviour
{
    public Food ThisFood;

    public int FoodPower = 10;
    public MonsterElement FoodElement;
    public string FoodCategory;
    public int SpriteIndex;

    public int inventorySpace;

    public Sprite[] NormalFoodSprites;
    /// <summary>
    /// Index: 0 - Air, 1 - Earth, 2 - Fire, 3 - Water
    /// </summary>
    public Sprite[] SpecialFoodSprites = new Sprite[4];
    Sprite ThisSprite;

    public Text UI;

    GameObject parent;
    GameManager manager;
    Animator MonsterAnim;
    public ParticleSystem Glow;

    bool isHeld;
    public bool IsInInventory;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        MonsterAnim = manager.MonsterObject.GetComponent<Animator>();
        parent = GameObject.Find("Canvas_Home");

        //checks what type of food it was and sets its sprite.
        if (FoodCategory == "Normal") 
        {
            ThisFood = new Food(-1);
            ThisSprite = NormalFoodSprites[SpriteIndex];
        }
        //Special Food types get particles colored to their element, sets the sprite.
        else
        {
            ThisFood = new Food(FoodElement, FoodPower);
            Glow.gameObject.SetActive(true);
            ParticleSystem.MainModule settings = Glow.main;
            switch (FoodElement)
            {
                case MonsterElement.Air:
                    ThisSprite = SpecialFoodSprites[0];
                    settings.startColor = new ParticleSystem.MinMaxGradient(Color.white);
                    break;
                case MonsterElement.Earth:
                    ThisSprite = SpecialFoodSprites[1];
                    settings.startColor = new ParticleSystem.MinMaxGradient(new Color(0.5372549f, 0.8941177f, 0.4392157f));
                    break;
                case MonsterElement.Fire:
                    ThisSprite = SpecialFoodSprites[2];
                    settings.startColor = new ParticleSystem.MinMaxGradient(new Color(0.9647059f, 0.4901961f, 0.345098f));
                    break;
                case MonsterElement.Water:
                    ThisSprite = SpecialFoodSprites[3];
                    settings.startColor = new ParticleSystem.MinMaxGradient(new Color(0.6078432f, 0.6705883f, 0.882353f));
                    break;
                default:
                    Debug.LogError("Element not found");
                    break;
            }
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = ThisSprite;
    }


    private void Update()
    {
        //sometimes doesnt work in start so its here in update
        if(manager.MonsterObject.GetComponent<Animator>() != MonsterAnim)
            MonsterAnim = manager.MonsterObject.GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Monster") && !isHeld && !manager.ActiveMonster.IsSleepingStatus && !MonsterAnim.GetBool("Eating") && !IsInInventory)
        {
            //updates the monsters hunger when it eats the food
            manager.ActiveMonster.UpdateHunger(manager.ActiveMonster.HungerStatus + ThisFood.Power);
            FindObjectOfType<SoundManager>().play("MunchSound");
            //Testing to heal when given special food.

            
            /*
            //spawns temporary feedback UI
            Text spawn = Instantiate(UI);
            spawn.text = "+" + ThisFood.Power;
            spawn.transform.SetParent(parent.transform, false);
            spawn.transform.localPosition = new Vector3(347f, 276f, 60f);
            */

            //destroys the food
            Destroy(gameObject);
            manager.FoodInventory[inventorySpace] = new Food();
            //adds the last special food element to the monster
            if (ThisFood.FoodType == "Special")
                manager.ActiveMonster.Element = ThisFood.Element;
            //plays the monsters eating animation.
            if (MonsterAnim != null)
                MonsterAnim.SetBool("Eating", true);

            //Personality dependancies
            if (manager.ActiveMonster.Personality.Equals(MonsterType.Hungry))
                manager.ActiveMonster.AddHappiness(FoodPower);
            else
                manager.ActiveMonster.AddHappiness(FoodPower/2);

            if (manager.ActiveMonster.Personality != MonsterType.Fighter)
                manager.ActiveMonster.UpdateHealth(manager.ActiveMonster.HealthStatus + manager.ActiveMonster.GetMaxHealth / 3);
            else
                manager.ActiveMonster.UpdateHealth(manager.ActiveMonster.HealthStatus + manager.ActiveMonster.GetMaxHealth / 5);

        }
    }

    private void OnMouseDown()
    {
        isHeld = true;
        IsInInventory = false;
    }

    private void OnMouseUp()
    {
        isHeld = false;
    }
}

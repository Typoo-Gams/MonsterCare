using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FoodManager_FoodObject : MonoBehaviour
{
    Food ThisFood;

    public int FoodPower = 10;
    public string FoodElement;
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

    Canvas CurrentCanvas;
    GameManager manager;
    public ParticleSystem Glow;

    // Start is called before the first frame update
    void Start()
    {



        //This can be moved to the spawn object button
        //CurrentCanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();



        //finds the __app for referencing the gamemanager. finds the button that spawns food.
        //manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();

        //gets the food info
        GetFoodInfo();
        //checks what type of food it was and sets its sprite.
        if (FoodCategory == "Normal") 
        {
            ThisFood = new Food(false);
            ThisSprite = NormalFoodSprites[SpriteIndex];
        }
        else
        {
            ThisFood = new Food(FoodElement, FoodPower);
            Glow.gameObject.SetActive(true);
            ParticleSystem.MainModule settings = Glow.main;
            switch (FoodElement)
            {
                case "Air":
                    ThisSprite = SpecialFoodSprites[0];
                    settings.startColor = new ParticleSystem.MinMaxGradient(Color.white);
                    break;
                case "Earth":
                    ThisSprite = SpecialFoodSprites[1];
                    settings.startColor = new ParticleSystem.MinMaxGradient(new Color(0.5372549f, 0.8941177f, 0.4392157f));
                    break;
                case "Fire":
                    ThisSprite = SpecialFoodSprites[2];
                    settings.startColor = new ParticleSystem.MinMaxGradient(new Color(0.9647059f, 0.4901961f, 0.345098f));
                    break;
                case "Water":
                    ThisSprite = SpecialFoodSprites[3];
                    settings.startColor = new ParticleSystem.MinMaxGradient(new Color(0.6078432f, 0.6705883f, 0.882353f));
                    break;
            }
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = ThisSprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Monster")) 
        {
            //updates the monsters hunger when it eats the food
            manager.ActiveMonster.UpdateHunger(manager.ActiveMonster.HungerStatus + ThisFood.Power);
            FindObjectOfType<SoundManager>().play("MunchSound");

            //spawns temporary feedback UI
            Text spawn = Instantiate(UI);
            spawn.text = "+" + ThisFood.Power;
            spawn.transform.SetParent(CurrentCanvas.transform, false);
            spawn.transform.localPosition = new Vector3(347f, 276f, 60f);
            
            //destroys the food
            Destroy(gameObject);
            manager.FoodInventory[inventorySpace] = new Food(true);
            //adds the last special food element to the monster
            if (ThisFood.FoodType == "Special")
                manager.ActiveMonster.Element = ThisFood.Element;
            //plays the monsters eating animation.
            //temporary
            if (manager.MonsterObject.GetComponent<DefaultStarting_MonsterController>() != null)
                manager.MonsterObject.GetComponent<Animator>().Play("Child_Eating");  //needs generic reference
        }
    }


    void GetFoodInfo() 
    {
        //FoodPower = manager.FoodInventory[inventorySpace].Power;
        //FoodElement = manager.FoodInventory[inventorySpace].Element;
        //FoodCategory = manager.FoodInventory[inventorySpace].FoodType;
    }
}

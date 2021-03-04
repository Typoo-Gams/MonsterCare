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

    SpawnItem_Button SpawningButton;




    // Start is called before the first frame update
    void Start()
    {



        //This can be moved to the spawn object button
        try
        {
            CurrentCanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        }
        catch 
        {
            CurrentCanvas = GameObject.FindGameObjectWithTag("CanvasFighting").GetComponent<Canvas>();
        }
        gameObject.transform.localScale = new Vector3(100, 100, 100);
        gameObject.transform.localPosition = new Vector3(-118, 204, 0);

        //finds the __app for referencing the gamemanager. finds the button that spawns food.
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();

        //gets the food info
        GetFoodInfo();
        //checks what type of food it was and sets its sprite.
        if (FoodCategory == "Normal") 
        {
            ThisFood = new Food();
            ThisSprite = NormalFoodSprites[12];
        }
        else
        {
            ThisFood = new Food(FoodElement, FoodPower);

            switch (FoodElement)
            {
                case "Air":
                    ThisSprite = SpecialFoodSprites[0];
                    break;
                case "Earth":
                    ThisSprite = SpecialFoodSprites[1];
                    break;
                case "Fire":
                    ThisSprite = SpecialFoodSprites[2];
                    break;
                case "Water":
                    ThisSprite = SpecialFoodSprites[3];
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
            manager.ActiveMonster.UpdateHunger(manager.ActiveMonster.HungerStatus + FoodPower);

            //spawns temporary feedback UI
            Text spawn = Instantiate(UI);
            spawn.text = "+" + FoodPower;
            spawn.transform.SetParent(CurrentCanvas.transform, false);
            spawn.transform.localPosition = new Vector3(347f, 276f, -19439.998f);
            spawn.transform.localScale = new Vector3(1,1,1);
            
            //destroys the food
            Destroy(gameObject);
            manager.FoodInventory[inventorySpace] = null;
            //plays the monsters eating animation.
            //temporary
            if (manager.MonsterObject.GetComponent<DefaultStarting_MonsterController>() != null)
                manager.MonsterObject.GetComponent<Animator>().Play("Child_Eating");  //needs generic reference
        }
    }


    void GetFoodInfo() 
    {
        FoodPower = manager.FoodInventory[inventorySpace].Power;
        FoodElement = manager.FoodInventory[inventorySpace].Element;
        FoodCategory = manager.FoodInventory[inventorySpace].FoodType;
    }
}

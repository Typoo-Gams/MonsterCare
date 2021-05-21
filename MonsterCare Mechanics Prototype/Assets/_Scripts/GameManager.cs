using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;


public class GameManager : MonoBehaviour
{
    public bool DevBuild;

    //Previously loaded scene
    public int PreviousSecene;
    public bool SleepMemory;

    //for checking which scene it is
    public int sceneNumber;
    public string sceneName;

    //Animator for fade scene transitions
    public Animator Fade;

    //GameSaver for saving info.
    GameSaver Save = new GameSaver();

    //Currently Active Monster
    public Monster ActiveMonster;
    public GameObject MonsterObject;
    [HideInInspector]
    public float MonsterUpdateSpeed = 0.1f;

    //Currently Active Enemy Monster
    public GameObject Enemy;
    public Monster EnemyMonster;

    //slider for stuff
    public GameObject HealthBarPrefab;

    //GameVersion
    [NonSerialized]
    public string GameVersion;

    //Player Inventory
    public Food[] FoodInventory = new Food[5];

    //latest reward
    public Food FoodReward;

    //Remember if this is a new game.
    public bool NewSave, Tutorial;

    //Hide UI
    public bool HideUI;

    //Awake is called when the script instance is being loaded
    private void Awake()
    {
        Tutorial = !Save.IsTutorialDone();
        GameVersion = "20.1";
        Debug.LogWarning("GameVersion is V." + GameVersion);
        FoodInventory = new Food[]{
            new Food(true),
            new Food(true),
            new Food(true),
            new Food(true),
            new Food(true)
        };
        FoodInventory = Save.LoadFood();
    }


    //Start is called just before any of the Update methods is called the first time
    private void Start()
    {
        //temporary
        FoodInventory[0] = new Food(false, 1);
        FoodInventory[1] = new Food(MonsterElement.Air);
        FoodInventory[2] = new Food(MonsterElement.Fire);
        FoodInventory[3] = new Food(MonsterElement.Earth);
        FoodInventory[4] = new Food(MonsterElement.Water);
        Save.SaveFood(FoodInventory);
        Save.PrintObtainedMonsters();
    }


    private void Update() 
    {
        if (SceneManager.GetActiveScene().name == "MonsterHome")
        {
            //Dev cheats
            if (Input.GetKeyDown(KeyCode.I))
            {
                ActiveMonster.UpdateHealth(10000);
                ActiveMonster.HappinessStatus = 10000;
                ActiveMonster.UpdateHappiness();
                ActiveMonster.UpdateHunger(10000);
                ActiveMonster.SleepStatus = 10000;
                ActiveMonster.UpdateSleeping(false);
                ActiveMonster.EnergyStatus = 1000;

                FoodInventory[0] = new Food(false);
                FoodInventory[0].Sprite = 12;
                FoodInventory[1] = new Food(MonsterElement.Air);
                FoodInventory[2] = new Food(MonsterElement.Fire);
                FoodInventory[3] = new Food(MonsterElement.Earth);
                FoodInventory[4] = new Food(MonsterElement.Water);
                Save.SaveFood(FoodInventory);
                Debug.Log("Cheat I activated");
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                ActiveMonster.HealthStatus = 1;
                //ActiveMonster.UpdateHealth(1);
                //ActiveMonster.UpdateHunger(0);
                ActiveMonster.HungerStatus = 0;
                ActiveMonster.EnergyStatus = 0;
                Debug.Log("Cheat O activated");
            }

            if(Input.GetKeyDown(KeyCode.P))
            {
                ActiveMonster.EnergyStatus = 500;
                ActiveMonster.HungerStatus = 5000;
                Debug.Log("Cheat P activated");
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                Save.SaveNote(1, 1);
                Save.SaveNote(2, 1);
                Save.SaveNote(3, 1);
                Save.SaveNote(4, 1);
            }


        }
    }


    //When the application is killed then save
    private void OnApplicationQuit()
    {
        try
        {
            Save.SaveTime();
            Save.SaveMonster(ActiveMonster);
            Save.SaveFood(FoodInventory);
            Save.SaveGameVersion(GameVersion);
        }
        catch
        {
            Debug.LogError("Something went wrong when saving");
        }
    }
    
    //When the application is paused then save.
    private void OnApplicationPause(bool focus)
    {
        if (focus)
        {
            try
            {
                Save.SaveTime();
                Save.SaveMonster(ActiveMonster);
                Save.SaveFood(FoodInventory);
                Save.SaveGameVersion(GameVersion);
                //Debug.Log("Saved with pause");
            }
            catch
            {
                Debug.LogError("Something went wrong when saving");
            }
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            try
            {
                Save.SaveTime();
                Save.SaveMonster(ActiveMonster);
                Save.SaveFood(FoodInventory);
                Save.SaveGameVersion(GameVersion);
                //Debug.Log("Saved with pause");
            }
            catch
            {
                Debug.LogError("Something went wrong when saving");
            }
        }
    }

    //Called when a new scene is loaded.
    private void OnLevelWasLoaded(int level)
    {
        if (MonsterObject != null) 
        {
            if (SceneManager.GetActiveScene().name == "MonsterHome")
            {
                //change to renderer so that stats can change while in other scenes?
                MonsterObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {

                //change to renderer so that stats can change while in other scenes?
                MonsterObject.GetComponent<SpriteRenderer>().enabled = false;

            }
        }

        //Debug.Log("previous scene: " + PreviousSecene);
    }


    //Set active monster so the gamemanager knows the monsters stats.
    /// <summary>
    /// Set the active monster
    /// </summary>
    /// <param name="yourMonster">Your monster</param>
    public void GetActiveMonster(Monster yourMonster) 
    {
        ActiveMonster = yourMonster;
    }


    //Set the gameobject for the active monster so the gamemanager has acess to it.
    /// <summary>
    /// set the gameobject for the active monster
    /// </summary>
    /// <param name="yourMonster">your monster gameobject</param>
    public void GetObjMonster(GameObject yourMonster)
    {
        MonsterObject = yourMonster;
    }
}
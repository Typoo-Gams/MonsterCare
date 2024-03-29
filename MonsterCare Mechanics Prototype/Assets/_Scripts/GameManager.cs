using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
    bool TutorialIsActive, tutorialReturned;
    public GameObject RadioTutorial;

    //Hide UI
    public bool HideUI;

    //Awake is called when the script instance is being loaded
    private void Awake()
    {
        Tutorial = !Save.IsTutorialDone();
        //Debug.LogError(Save.IsTutorialDone());
        GameVersion = "23.4_SemesterHandIn";
        Debug.LogWarning("GameVersion is V." + GameVersion);
        FoodInventory = new Food[]{
            new Food(),
            new Food(),
            new Food(),
            new Food(),
            new Food()
        };
        FoodInventory = Save.LoadFood();
    }


    //Start is called just before any of the Update methods is called the first time
    private void Start()
    {
        /*
        //two random foods on new game
        FoodInventory[0] = new Food(UnityEngine.Random.Range(0, 12));
        FoodInventory[1] = new Food(MonsterElement.Water);
        FoodInventory[2] = new Food(MonsterElement.Fire);
        FoodInventory[3] = new Food(MonsterElement.Air);
        FoodInventory[4] = new Food(MonsterElement.Earth);
        Save.SaveFood(FoodInventory);
        */
    }


    private void Update() 
    {
        if (SceneManager.GetActiveScene().name == "MonsterHome")
        {
            //Dev cheats
            /*
            if (Input.GetKeyDown(KeyCode.I))
            {
                ActiveMonster.UpdateHealth(ActiveMonster.GetMaxHealth);
                ActiveMonster.HappinessStatus = ActiveMonster.GetMaxHappiness;
                ActiveMonster.UpdateHappiness();
                ActiveMonster.UpdateHunger(ActiveMonster.GetMaxHunger);
                ActiveMonster.SleepStatus = ActiveMonster.GetMaxSleep;
                ActiveMonster.UpdateSleeping(false);
                ActiveMonster.EnergyStatus = ActiveMonster.GetMaxEnergy;

                FoodInventory[0] = new Food(12);
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
            */

            //Second part of the tutorial
            if (Tutorial)
            {
                if (Save.GetTutorialStage() == 2 && !TutorialIsActive)
                {
                    TutorialIsActive = true;
                    Animator Notes = null;
                    foreach (GameObject found in GameObject.FindGameObjectsWithTag("Tutorial"))
                    {
                        if (found.GetComponent<Button>() != null && !found.name.Equals("NotesButton"))
                            found.GetComponent<Button>().interactable = false;
                    }
                    foreach (GameObject found in GameObject.FindGameObjectsWithTag("Tutorial"))
                    {
                        if (found.name.Equals("NotesButton"))
                            Notes = found.GetComponent<Animator>();
                    }
                    if(Notes != null)
                        Notes.SetBool("Pointer", true);
                }
            }

            //Returns tutorial to where the player left of if it wasnt completed
            if (!tutorialReturned)
            {
                tutorialReturned = true;
                switch (Save.GetTutorialStage())
                {
                    case 0:
                        HideUI = true;
                        foreach (GameObject found in GameObject.FindGameObjectsWithTag("Tutorial"))
                        {
                            if (found.GetComponent<Button>() != null)
                            {
                                found.GetComponent<Button>().interactable = false;
                            }
                        }
                        break;

                    case 1:
                        HideUI = true;
                        foreach (GameObject found in GameObject.FindGameObjectsWithTag("Tutorial"))
                        {
                            if (found.GetComponent<Button>() != null)
                            {
                                found.GetComponent<Button>().interactable = false;
                            }
                        }
                        HideUI = true;
                        Instantiate(RadioTutorial).transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
                        break;
                }
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
                Debug.LogError("Saved");
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
        else
        {
            //Debug.LogError("resumed pause: " + Save.FindTimeDifference());
            if (ActiveMonster != null)
                ActiveMonster.AtGameWakeUp(Save.FindTimeDifference());
        }
    }

    /*
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            try
            {
                Debug.LogError("Saved");
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
        else
        {
            Debug.LogError("resumed focus: " + Save.FindTimeDifference());
            ActiveMonster.AtGameWakeUp(Save.FindTimeDifference());
        }
    }
    */

    //Called when a new scene is loaded.
    private void OnLevelWasLoaded(int level)
    {
        if (MonsterObject != null) 
        {
            if (SceneManager.GetActiveScene().name == "MonsterHome")
            {
                //change to renderer so that stats can change while in other scenes?
                MonsterObject.GetComponent<SpriteRenderer>().enabled = true;
                if (MonsterObject.transform.childCount > 0)
                {
                    for (int i = 0; i < MonsterObject.transform.childCount; i++)
                    {
                        if (MonsterObject.transform.GetChild(i) != null)
                        {
                            MonsterObject.transform.GetChild(i).gameObject.SetActive(true);
                        }
                    }
                }
            }
            else
            {

                //change to renderer so that stats can change while in other scenes?
                MonsterObject.GetComponent<SpriteRenderer>().enabled = false;
                if (MonsterObject.transform.childCount > 0)
                {
                    for (int i = 0; i < MonsterObject.transform.childCount; i++)
                    {
                        if (MonsterObject.transform.GetChild(i) != null)
                        {
                            MonsterObject.transform.GetChild(i).gameObject.SetActive(false);
                        }
                    }
                }
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
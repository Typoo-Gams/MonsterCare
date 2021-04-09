using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClearSave : MonoBehaviour
{
    GameManager manager;
    public bool ClearSaveScene;
    bool IsWiped;
    float cnt;
    public float WhaitTime;
    bool Delete;
    public GameObject whaitTimer, StartingMonster;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        if (gameObject.GetComponent<Button>() != null) 
        {
            gameObject.GetComponent<Button>().onClick.AddListener(ResetSave);
        }
        if (whaitTimer != null) 
            whaitTimer.GetComponent<LoadingBar>().whaitTime = WhaitTime;
    }

    public void ResetSave() 
    {
        //Try to destroy the monster objects. this could already have happened or it could not have been created yet so this will catch the error
        try
        {
            Destroy(manager.MonsterObject);
            manager.ActiveMonster = null;
            manager.MonsterObject = null;
            Delete = true;
        }
        catch
        {
            Debug.Log("No Monster was spawned yet to wipe from save.");
        }
        //wipes the savefile.
        GameSaver Saver = new GameSaver();
        Saver.WipeSave();
        if(Camera.main.GetComponentInChildren<MonsterLoader>() == null)
        {
            manager.FoodInventory = Saver.LoadFood();
            manager.NewSave = true;
            Saver.SaveObtainedMonster("None", false, true);
        }
    }

    private void Update()
    {
        if (ClearSaveScene)
        {
            cnt += Time.deltaTime;
            whaitTimer.GetComponent<LoadingBar>().CurrentWhaitTime = cnt;
        }

        //There if there is no activemonster in the preload scene then exit the application or if the clearsavescene is true then whait for the value of whaitTime
        if (Delete || cnt > WhaitTime)
        {
            if (!IsWiped) 
            {
                ResetSave();
                IsWiped = true;
                if (ClearSaveScene)
                {
                    GameObject SavedMonster = Resources.Load<GameObject>("Prefabs/MonsterStuff/Monsters/Gen 0/Child_Gen0");
                    GameObject SpawnedMonster = Instantiate(SavedMonster, SavedMonster.transform.position, Quaternion.identity);
                    SpawnedMonster.transform.SetParent(manager.gameObject.transform, false);

                    SceneManager.LoadScene("MainMenuScreen");
                }
                ClearSaveScene = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FolderViewer_Button : MonoBehaviour
{
    public enum Monsters
    {
        Child_Gen0,
        AirSleepy_Gen1,
        BeefMaster_Gen1,
        FireSleepy_Gen1,
        WaterPlayful_Gen1,
        Empty
    }

    public enum Background
    {
        Fire,
        Water,
        Air,
        Earth,
        Basic
    }


    GameSaver Saver = new GameSaver();
    public GameObject viewItem;
    public Transform currentPage;
    public Monsters Selection;
    public Background _Color;
    

    Button myButton;
    private Sprite NotObtained;

    public static GameObject CurrentView = null;
    public bool isFilled;

    public Sprite[] background = new Sprite[5];

    // Start is called before the first frame update
    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(TaskOnClick);
        NotObtained = myButton.image.sprite;

    }

    private void Update()
    {
        if (!Selection.Equals(Monsters.Empty))
        {
            if (Saver.MonsterObtainedBefore(Selection.ToString()) && NotObtained == myButton.image.sprite)
            {
                switch (_Color)
                {
                    case Background.Air:
                        myButton.image.sprite = background[0];
                        break;

                    case Background.Fire:
                        myButton.image.sprite = background[1];
                        break;

                    case Background.Earth:
                        myButton.image.sprite = background[2];
                        break;

                    case Background.Water:
                        myButton.image.sprite = background[3];
                        break;

                    case Background.Basic:
                        myButton.image.sprite = background[4];
                        break;
                }
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        if (CurrentView == null)
        {
            CurrentView = null;
            myButton.interactable = true;
            Debug.LogWarning("Interact = tru");
        }      
        
        if(CurrentView != null)
        {
            myButton.interactable = false;
            Debug.LogWarning("Interact = false");
        }
    }


    private void TaskOnClick()
    {
        if (CurrentView == null && Saver.MonsterObtainedBefore(Selection.ToString()))
        {
            GameObject spawn = Instantiate(viewItem);
            CurrentView = spawn;
            spawn.transform.SetParent(currentPage, false);
            spawn.GetComponent<Delete>().viewer = this;
        }
    }
}

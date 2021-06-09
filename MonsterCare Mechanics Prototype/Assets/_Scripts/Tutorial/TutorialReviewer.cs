using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialReviewer : MonoBehaviour
{
    Image _thisImage;
    public Sprite[] Page;
    int pageNum = 0;

    GameSaver Saver = new GameSaver();
    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        _thisImage = gameObject.GetComponent<Image>();
        if (!Saver.IsTutorialDone())
        {
            Saver.IsTutorialDone(3);
            manager.HideUI = false;
        }
    }

    private void Update()
    {
        //i dont know why but sometimes these saves dont save so im doing this instead temporary
        if (Saver.GetTutorialStage() != 3)
        {
            Saver.IsTutorialDone(3);
        }
    }

    private void OnMouseDown()
    {
        //All Pages have been viewed
        if (Page[Page.Length - 1] == _thisImage.sprite)
        {
            Destroy(gameObject);
        }
        else
        {
            //Next Page
            pageNum++;
            _thisImage.sprite = Page[pageNum];
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadedNotes_Button : MonoBehaviour
{
    public GameObject NotePrefab, HasNotBeenSeen;
    public int NoteNumber;
    bool IsObtained;
    public Transform currentPage;
    Button myButton;
    public Sprite Obtained;
    Text ButtonNumber;

    GameSaver Saver = new GameSaver();

    public static GameObject CurrentView;

    // Start is called before the first frame update
    void Start()
    {
        if (!Saver.GetSeenNote(NoteNumber) && Saver.LoadNote(NoteNumber) == 1)
        {
            HasNotBeenSeen.SetActive(true);
        }
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(TaskOnClick);
        ButtonNumber = GetComponentInChildren<Text>();
        ButtonNumber.text = NoteNumber + "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Saver.LoadNote(NoteNumber) == 1 && !IsObtained) 
        {
            IsObtained = true;
            myButton.image.sprite = Obtained;
            ButtonNumber.enabled = true;
        }

        if (CurrentView == null)
        {
            CurrentView = null;
            myButton.interactable = true;
        }

        if (CurrentView != null)
        {
            myButton.interactable = false;
        }
    }

    private void TaskOnClick()
    {
        if (IsObtained && CurrentView == null)
        {
            Saver.SetSeenNote(NoteNumber, true);
            HasNotBeenSeen.SetActive(false);
            CurrentView = Instantiate(NotePrefab);
            CurrentView.transform.SetParent(currentPage, false);
        }
    }
}

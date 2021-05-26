using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTutorialReports : MonoBehaviour
{
    Button _thisButton;
    List<Button> OtherButtons = new List<Button>();

    public GameObject CurrentView = null;
    public Transform currentPage;
    public GameObject viewItem;
    bool OperationDone;

    // Start is called before the first frame update
    void Start()
    {
        _thisButton = gameObject.GetComponent<Button>();
        _thisButton.onClick.AddListener(TaskOnClick);

        foreach (Button found in GameObject.FindObjectsOfType<Button>())
        {
            if (!found.name.Equals(gameObject.name))
            {
                OtherButtons.Add(found);
            }
        }
    }

    // TaskOnClick is called when the button is pressed
    private void TaskOnClick()
    {
        if (CurrentView == null)
        {
            GameObject spawn = Instantiate(viewItem);
            CurrentView = spawn;
            spawn.transform.SetParent(currentPage, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentView != null && !OperationDone)
        {
            foreach (Button found in OtherButtons)
            {
                found.interactable = false;
            }
            OperationDone = true;
        }
        if (CurrentView == null)
        {

        }
    }
}

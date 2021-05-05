using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFolder_Button : MonoBehaviour
{
    public List<GameObject> currentPage;
    public List<GameObject> otherPages;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        foreach(GameObject found in currentPage)
        {
            found.SetActive(true);
        }

        foreach (GameObject found in otherPages)
        {
            found.SetActive(false);
        }
    }
}

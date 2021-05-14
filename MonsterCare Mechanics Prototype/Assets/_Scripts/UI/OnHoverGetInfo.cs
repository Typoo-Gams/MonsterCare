using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OnHoverGetInfo : MonoBehaviour
{
    public GameObject Panel;
    GameObject spawn;
    public string TextToDisplay;
    private string startText;
    public bool ShowInfo = true, OverWrite;
    Image fill;


    // Start is called before the first frame update
    void Start()
    {
        startText = TextToDisplay;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Equals("Fill"))
            {
                fill = transform.GetChild(i).GetComponent<Image>();
                break;
            }
        }
    }

    private void OnMouseEnter()
    {
        
    }

    private void OnMouseExit()
    {
        if (spawn != null)
        {
            Destroy(spawn);
            ShowInfo = true;
        }
    }

    private void OnMouseOver()
    {
        if (ShowInfo && !OverWrite)
        {
            string persentage = Math.Truncate(fill.fillAmount * 100) + ""; // fill.fillAmount * 100 + "";

            spawn = Instantiate(Panel);
            spawn.transform.SetParent(gameObject.transform, false);
            spawn.GetComponentInChildren<Text>().text = TextToDisplay + ": " + persentage + "%";
            spawn.transform.localPosition = new Vector3(-4f, 0, 0);
            ShowInfo = false;
        }
    }
}

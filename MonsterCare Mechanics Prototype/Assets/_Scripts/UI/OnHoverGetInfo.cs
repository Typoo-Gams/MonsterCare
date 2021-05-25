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
    public bool ShowInfo = true, OverWrite, ShowElement;
    Image fill, element;


    // Start is called before the first frame update
    void Start()
    {
        startText = TextToDisplay;
        if (!ShowElement)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name.Equals("Fill"))
                {
                    fill = transform.GetChild(i).GetComponent<Image>();
                    break;
                }
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
        if (ShowInfo && !OverWrite && !ShowElement)
        {
            string persentage = Math.Truncate(fill.fillAmount * 100) + ""; // fill.fillAmount * 100 + "";

            spawn = Instantiate(Panel);
            spawn.transform.SetParent(gameObject.transform, false);
            spawn.GetComponentInChildren<Text>().text = TextToDisplay + ": " + persentage + "%";
            spawn.transform.localPosition = new Vector3(-4f, 0, 0);
            ShowInfo = false;
        }
        if (ShowInfo && !OverWrite && ShowElement)
        {
            if (gameObject.GetComponent<Image>() != null)
            {
                spawn = Instantiate(Panel);
                spawn.transform.SetParent(gameObject.transform, false);

                switch (gameObject.GetComponent<Image>().sprite.name)
                {
                    case "FireIcon":
                        spawn.GetComponentInChildren<Text>().text = "Fire";
                        break;

                    case "waaterIcon":
                        spawn.GetComponentInChildren<Text>().text = "Water";
                        break;

                    case "AirIcon":
                        spawn.GetComponentInChildren<Text>().text = "Air";
                        break;

                    case "EarthIcon":
                        spawn.GetComponentInChildren<Text>().text = "Earth";
                        break;
                }
                spawn.transform.localPosition = new Vector3(-4f, 0, 0);
                ShowInfo = false;
            }
        }
    }
}

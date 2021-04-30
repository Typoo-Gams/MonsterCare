using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnHoverGetInfo : MonoBehaviour
{
    public GameObject Panel;
    GameObject spawn;
    public string TextToDisplay;
    public bool ShowInfo = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseEnter()
    {
        
    }

    private void OnMouseExit()
    {
        if (spawn != null)
        {
            Destroy(spawn);
        }
    }

    private void OnMouseOver()
    {
        if (ShowInfo)
        {
            spawn = Instantiate(Panel);
            spawn.transform.SetParent(gameObject.transform, false);
            spawn.GetComponentInChildren<Text>().text = TextToDisplay;
            spawn.transform.localPosition = new Vector3(-4f, 0, 0);
        }
    }
}

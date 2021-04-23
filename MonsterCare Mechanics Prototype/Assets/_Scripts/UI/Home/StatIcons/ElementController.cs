using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementController : MonoBehaviour
{
    private Image Current;
    public Image AnimImage;

    // Start is called before the first frame update
    void Start()
    {
        Current = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Current.sprite != AnimImage.sprite)
        {
            AnimImage.sprite = Current.sprite;
        }
    }
}

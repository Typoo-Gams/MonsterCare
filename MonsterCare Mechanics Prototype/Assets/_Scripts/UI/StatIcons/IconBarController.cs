using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconBarController : MonoBehaviour
{
    public Image IconFill;
    public float FillAmmount;

    // Start is called before the first frame update
    void Start()
    {
        IconFill.fillOrigin = 0;
    }

    // Update is called once per frame
    void Update()
    {
        IconFill.fillAmount = FillAmmount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTipGenerator : MonoBehaviour
{
    int CurrentTip;
    Animator Tips;
    public int TipCount;

    // Start is called before the first frame update
    void Start()
    {
        Tips = GetComponent<Animator>();
        CurrentTip = Random.Range(0, TipCount);
        Tips.SetInteger("TipNumber", CurrentTip);
    }
}

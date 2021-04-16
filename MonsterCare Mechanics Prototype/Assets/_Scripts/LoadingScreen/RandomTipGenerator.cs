using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTipGenerator : MonoBehaviour
{
    int CurrentTip;
    public GameObject[] Tips;

    // Start is called before the first frame update
    void Start()
    {
        CurrentTip = Random.Range(0, Tips.Length);
        Tips[CurrentTip].SetActive(true);
    }
}

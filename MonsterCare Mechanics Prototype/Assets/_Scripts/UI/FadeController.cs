using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    Animator anim;
    bool StateChanged;
    bool DisableUI = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("FadeOut") && !StateChanged) 
        {
            StateChanged = true;
            foreach (Button found in FindObjectsOfType<Button>())
            {
                found.interactable = false;
            }
        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("EvolutionFadeOut") && !StateChanged)
        {
            StateChanged = true;
            foreach (Button found in FindObjectsOfType<Button>())
            {
                found.interactable = false;
                //Debug.Log(found.gameObject.name);
            }
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("EvolutionFadeIn") && StateChanged)
        {
            StateChanged = false;
            foreach (Button found in FindObjectsOfType<Button>())
            {
                found.interactable = true;
                //Debug.Log(found.gameObject.name);
            }
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        StateChanged = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightInteraction : MonoBehaviour
{
    Animator goblinAnim;
    Goblin Controller;
    public bool IsBag;

    // Start is called before the first frame update
    void Start()
    {
        Controller = gameObject.GetComponentInParent<Goblin>();
        goblinAnim = gameObject.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (IsBag && Controller.Vulnerable) 
        {
            Controller.Health -= 2;
            Debug.Log("Doubble dmg");
        }
        else
            Controller.Health--;   
    }
}

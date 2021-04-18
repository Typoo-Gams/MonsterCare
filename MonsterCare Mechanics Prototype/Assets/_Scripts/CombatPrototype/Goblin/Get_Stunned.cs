using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_Stunned : MonoBehaviour
{
    Animator goblinAnim;
    Goblin Controller;

    // Start is called before the first frame update
    void Start()
    {
        Controller = gameObject.GetComponentInParent<Goblin>();
        goblinAnim = gameObject.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.HeadHealth == 0)
            Controller.Stunned = true;
        else
            Controller.Stunned = false;
    }

    private void OnMouseDown()
    {
        if(goblinAnim.GetCurrentAnimatorStateInfo(0).IsName("default state") && !Controller.Vulnerable)
        {
            //goblinAnim.Play();
            if(Controller.HeadHealth > 0 && !Controller.Vulnerable)
                Controller.HeadHealth--;
        }
        //temporary
        if (Controller.HeadHealth > 0 && !Controller.Vulnerable)
            Controller.HeadHealth--;
    }
}

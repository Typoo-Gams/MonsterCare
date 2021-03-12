using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpiredAttackAnim : MonoBehaviour
{
    public Animator attack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(attack.GetCurrentAnimatorStateInfo(0).IsName("EndOfAnimation"));

        if (attack.GetCurrentAnimatorStateInfo(0).IsName("EndOfAnimation"))
        {
            Destroy(gameObject);
        }
    }
}

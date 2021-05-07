using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDust : MonoBehaviour
{
    public GameObject smokeParticles;
    public GameObject Monster;
    //public ParticleSystem Smoke;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

       
    }
   
    public void Death()
    {
        //GameObject smokeParticlesPrefab = 
        for(int i = 0; i < 2; i++ )
        {
            Instantiate(smokeParticles, new Vector3(0f,-3f,0f), Quaternion.identity);
        }
            
      //  smokeParticlesPrefab.transform.position = new Vector3(0f, -143f, 0f);

        // DeathPoff.Play();
        Debug.Log("Poof!");
        //smokeParticlesPrefab.transform.SetParent(gameObject.transform, true);
    }
}

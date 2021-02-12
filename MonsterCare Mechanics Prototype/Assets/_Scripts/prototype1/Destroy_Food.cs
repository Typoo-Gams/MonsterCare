using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Food : MonoBehaviour
{
    public GameObject food;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("food"))
        {
            Destroy(gameObject);
        }
    }


}

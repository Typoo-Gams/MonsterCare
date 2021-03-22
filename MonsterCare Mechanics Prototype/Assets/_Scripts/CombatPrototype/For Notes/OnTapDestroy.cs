using UnityEngine;
using System.Collections;
public class OnTapDestroy : MonoBehaviour
{
    public GameObject noteIcon;
    bool spawned;
    float delay = 2f;

    private void Update()
    {
        if (spawned)
        {
            Destroy(noteIcon, delay);
            spawned = false;
        }
        NoteTaps();
    }

    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(2);
        if (Input.GetMouseButtonDown(0))
        {
            if (!spawned)
            {
                Instantiate(noteIcon);
                spawned = true;
            }
            Destroy(gameObject);
        }
    }

    private void NoteTaps()
    {
        StartCoroutine(Waiting());
    }
}

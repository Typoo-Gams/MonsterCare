using UnityEngine;
using System.Collections;
public class OnTapDestroy : MonoBehaviour
{
    public GameObject noteIcon;
    bool spawned;

    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(2);
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
            if (!spawned)
            {
                Instantiate(noteIcon);
                spawned = true;
            }
        }
    }

    private void Update()
    {
        NoteTaps();
        if (spawned)
        {
            noteIcon.SetActive(false);
            spawned = false;
        }
    }

    private void NoteTaps()
    {
        StartCoroutine(Waiting());
    }
}

using UnityEngine;
using System.Collections;
public class OnTapDestroy : MonoBehaviour
{
    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(2);
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);

        }
    }

    private void Update()
    {
        NoteTaps();
    }

    private void NoteTaps()
    {
        StartCoroutine(Waiting());
    }
}

using UnityEngine;
using System.Collections;
public class OnTapDestroy : MonoBehaviour
{
    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(3);
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        StartCoroutine(Waiting());
        Debug.LogWarning("WORKING!");
    }
}

using UnityEngine;
using System.Collections;
public class OnTapDestroy : MonoBehaviour
{
    public GameObject Taptext;

    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(2);
        Taptext.SetActive(true);
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        NoteTaps();
        Taptext.SetActive(false);
        Debug.LogWarning("WORKING!");
    }

    private void NoteTaps()
    {
        StartCoroutine(Waiting());
    }
}

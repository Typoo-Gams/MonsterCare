using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Image HealthBarFill;
    public Monster ThisMonster;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(ThisMonster.Name);
        float index = ThisMonster.HealthStatus / ThisMonster.GetMaxHealth;
        HealthBarFill.fillAmount = index;
    }
}

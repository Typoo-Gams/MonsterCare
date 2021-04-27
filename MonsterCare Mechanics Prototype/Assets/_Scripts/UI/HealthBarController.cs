using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public bool PlayerHealthBar;
    public Image HealthBarFill;
    public Image HealthIcon;
    public Monster ThisMonster;
    public Animator healthAnim;
    public Sprite[] healthIcons;

    // Update is called once per frame
    void Update()
    {
        float index = ThisMonster.HealthStatus / ThisMonster.GetMaxHealth;
        HealthBarFill.fillAmount = index;

        if (PlayerHealthBar)
        {
            if (HealthBarFill.fillAmount < 0.3f)
            {
                HealthIcon.sprite = healthIcons[2];
                healthAnim.SetBool("IsLow", true);
            }
            if (HealthBarFill.fillAmount < 0.6f && HealthBarFill.fillAmount > 0.3f)
            {
                HealthIcon.sprite = healthIcons[1];
                healthAnim.SetBool("IsLow", false);
            }
            if (HealthBarFill.fillAmount > 0.6f)
            {
                HealthIcon.sprite = healthIcons[0];
                healthAnim.SetBool("IsLow", false);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatReset : MonoBehaviour
{
    public MonsterManager_AttackPrototype KillThisMonster;

    float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = KillThisMonster.StartMonster.HealthStatus;
    }

    private void Update()
    {
        Death();
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(9);
    }

    public void Death()
    {
        if (KillThisMonster.StartMonster.DeathStatus)
        {
            StartCoroutine(Wait());
        }
    }
}
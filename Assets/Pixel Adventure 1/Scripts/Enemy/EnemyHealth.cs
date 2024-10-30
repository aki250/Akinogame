using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth, maxHealth, healthRegen;

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        Heal(healthRegen * Time.deltaTime);
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth+amount,maxHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0.0f);

        if(currentHealth <= 0)
        {
            Die();
        }
    }
    public float GetPercentage()
    {
        return currentHealth /maxHealth;
    }

    public void Die()
    {
        Debug.Log("enemy is Dead");
    }
}

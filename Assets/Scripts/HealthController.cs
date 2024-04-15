using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public HealthBar healthBar;

    public void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateMaximumValue(maxHealth);
    }

    public void Update()
    {
        healthBar.SetValue(currentHealth);
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            currentHealth -= other.GetComponent<Enemy>().damageToPlayer;
            
            // destroy enemy once it damages player
            Destroy(other.gameObject);
        }
    }
}

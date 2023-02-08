using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbarattachment : MonoBehaviour
{
    public int maxHealth = 100;
    public healthbar healthBar;

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Shell"))
        {
            TakeDamage(20);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
        

}

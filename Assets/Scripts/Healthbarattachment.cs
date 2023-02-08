using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbarattachment : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private GameObject canvas;

    private healthbar healthBar;

    private int currentHealth;

    private void Awake()
    {
        healthBar = Instantiate(healthBarPrefab, canvas.transform).GetComponent<healthbar>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        healthBar.transform.position = gameObject.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Shell"))
        {
            if (collision.gameObject.GetComponent<bullet>().parent != gameObject)
            {
                TakeDamage(20);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
        

}

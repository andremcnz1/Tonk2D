using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Healthbarattachment : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject healthBarPrefab;

    private GameObject canvas;
    private healthbar healthBar;
    private int currentHealth;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>().gameObject;
        healthBar = Instantiate(healthBarPrefab, canvas.transform).GetComponent<healthbar>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        healthBar.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
            Destroy(healthBar.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Shell"))
        {
            if (collision.gameObject.GetComponent<Bullet>().GetParent() != gameObject)
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

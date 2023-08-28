using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;
    public Slider healthSlider;

    void Start()
    {
        health = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Health Box"))
        {
            if (health <= maxHealth - 20)
            {
                health += 20f;
            }
            else if (maxHealth - health < 20)
            {
                health += (maxHealth - health);
            }
            healthSlider.value = health;
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthSlider.value = health;
    }
}

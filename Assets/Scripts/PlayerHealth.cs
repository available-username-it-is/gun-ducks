using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;
    public TextMeshProUGUI healthInfo;

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
            healthInfo.text = health.ToString();
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthInfo.text = health.ToString();
    }
}

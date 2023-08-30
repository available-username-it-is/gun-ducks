using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    private void OnCollisionEnter(Collision collision)
    {
        damage = transform.parent.GetComponent<EnemyAi>().damage;
        Transform hitTransform = collision.transform;
        if (hitTransform.CompareTag("Player"))
        {

            Debug.Log("Hit player");
            hitTransform.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}

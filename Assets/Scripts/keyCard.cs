using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class keyCard : MonoBehaviour
{
    public GameObject doorCollider;
    public TextMeshProUGUI task;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            doorCollider.SetActive(true);
            task.SetText("-Proceed to the first floor.");
        }
    }
}

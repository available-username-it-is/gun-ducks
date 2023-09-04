using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossBehaviour : MonoBehaviour
{
    public GameObject bossHealthBar;
    public TextMeshProUGUI task;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            task.SetText("Try not to die");
            bossHealthBar.SetActive(true);
        }
    }
}

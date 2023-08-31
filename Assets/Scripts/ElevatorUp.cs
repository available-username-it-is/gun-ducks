using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorUp : MonoBehaviour
{
    [SerializeField] private Animator elevator = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            elevator.Play("Elevator up", 0, 0.0f);
        }

    }
}

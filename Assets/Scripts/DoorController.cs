using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;

    [SerializeField] private bool doorTrigger = false;
    [SerializeField] private bool doubleDoorTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy entered collider");
            if (doorTrigger)
            {
                myDoor.Play("glass_door_open", 0, 0.0f);
            }

            if (doubleDoorTrigger)
            {
                myDoor.Play("door_1_open", 0, 0.0f);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (doorTrigger)
            {
                myDoor.Play("glass_door_close", 0, 0.0f);
                
            }
        }
    }
}

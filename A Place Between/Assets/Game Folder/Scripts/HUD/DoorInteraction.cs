using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    PlayerControl player;
    Animator animator;
    bool doorOpen = true;
    bool playerInRange = false;

    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleDoor();
        }
    }

    void ToggleDoor()
    {
        if (doorOpen)
        {
            Debug.Log("Abrir");
            animator.SetBool("OpenDoor", true);
            animator.SetBool("CloseDoor", false);
            doorOpen = false;
        }
        else
        {
            Debug.Log("Fechar");
            animator.SetBool("OpenDoor", false);
            animator.SetBool("CloseDoor", true);
            doorOpen = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}

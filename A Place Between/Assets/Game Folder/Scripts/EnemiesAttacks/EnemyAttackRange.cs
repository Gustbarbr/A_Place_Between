using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{
    PlayerControl player;
    Animator animator;

    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        animator = GetComponentInParent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            animator.SetBool("Attack", true);
        }
    }
}

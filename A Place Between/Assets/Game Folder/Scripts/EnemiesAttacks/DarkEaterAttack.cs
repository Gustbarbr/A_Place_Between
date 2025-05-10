using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkEaterAttack : MonoBehaviour
{
    PlayerControl player;
    Animator animator;
    float attackCooldown = 1;
    public float attackTimer;

    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && attackTimer >= attackCooldown)
        {

            if (player.increaseHP == false)
                player.hpSlider.value -= 0.2f;
            else if (player.increaseHP == true)
                player.hpSlider.value -= 0.1f;

            attackTimer = 0;
        }

    }
}

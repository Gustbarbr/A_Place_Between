using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LurkerAttack : MonoBehaviour
{
    PlayerControl player;
    float attackCooldown = 1;
    public float attackTimer;

    void Start(){
        player = FindObjectOfType<PlayerControl>();
    }

    void Update(){
        attackTimer += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player") && attackTimer >= attackCooldown){

            if (player.increaseHP == false)
                player.hpSlider.value -= 0.2f;
            else if (player.increaseHP == true)
                player.hpSlider.value -= 0.1f;

            player.healthRegen = 0;

            attackTimer = 0;
        }
    }
}

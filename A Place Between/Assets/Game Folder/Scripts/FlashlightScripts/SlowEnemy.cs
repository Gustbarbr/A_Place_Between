using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemy : MonoBehaviour
{
    EnemyControl enemyMovement;

    void Start()
    {
        enemyMovement = FindObjectOfType<EnemyControl>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        enemyMovement.speed = enemyMovement.speed * 0.5f;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        enemyMovement.speed = enemyMovement.speed * 2;
    }
}

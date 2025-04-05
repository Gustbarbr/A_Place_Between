using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemy : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Pega o inigo com a tag referente e ebnquanto estiver na luz, ficará 50% mais lento
        if (collider.CompareTag("Enemy"))
        {
            EnemyControl enemyMovement = collider.GetComponent<EnemyControl>();
            enemyMovement.speed = enemyMovement.speed * 0.5f;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        // Quando sair da luz, recupera a velocidade
        if (collider.CompareTag("Enemy"))
        {
            EnemyControl enemyMovement = collider.GetComponent<EnemyControl>();
            enemyMovement.speed = enemyMovement.speed * 2;
        }
    }
}

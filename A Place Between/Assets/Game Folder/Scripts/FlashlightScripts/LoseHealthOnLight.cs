using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseHealthOnLight : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collider)
    {
        // Quando o inimigo entra na luz, ele perde vida
        if (collider.CompareTag("LoseHealthOnLight"))
        {
            EnemyControl enemyHealth = collider.GetComponent<EnemyControl>();
            enemyHealth.hp -= Time.deltaTime;

            if (enemyHealth.hp <= 1)
            {
                Destroy(enemyHealth.gameObject);
            }
        }
    }
}

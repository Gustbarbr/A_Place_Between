using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispelInvisibility : MonoBehaviour
{
    EnemyControl enemyMovement;

    void Start()
    {
        enemyMovement = FindObjectOfType<EnemyControl>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("InvisibleEnemy"))
        enemyMovement.GetComponent<SpriteRenderer>().enabled = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("InvisibleEnemy"))
        enemyMovement.GetComponent<SpriteRenderer>().enabled = false;
    }
}

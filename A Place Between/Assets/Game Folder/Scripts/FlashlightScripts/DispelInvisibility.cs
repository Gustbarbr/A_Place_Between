using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispelInvisibility : MonoBehaviour
{
    private SpriteRenderer enemySpriteRenderer;

    void Start()
    {
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Quando o inimigo entra na luz, se torna vis�vel
        if (collider.CompareTag("InvisibleEnemy"))
            // Torna o inimigo vis�vel
            collider.GetComponent<SpriteRenderer>().enabled = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        // Quando o inimigo sai da luz, se torna invis�vel
        if (collider.CompareTag("InvisibleEnemy"))
        // Torna o inimigo invis�vel novamente
        collider.GetComponent<SpriteRenderer>().enabled = false;
    }
}

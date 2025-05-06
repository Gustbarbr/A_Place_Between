using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAmmo : MonoBehaviour
{
    PlayerControl player;
    BulletAmount bulletAmount;

    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        bulletAmount = FindObjectOfType<BulletAmount>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.ammunation += Random.Range(1, 15);
            bulletAmount.UpdateAmmoText();
            Destroy(gameObject);
        }
    }
}


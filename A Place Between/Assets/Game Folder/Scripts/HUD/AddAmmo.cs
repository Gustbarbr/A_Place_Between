using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAmmo : MonoBehaviour
{
    PlayerControl player;

    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.ammunation += Random.Range(1, 10);
        }
    }
}

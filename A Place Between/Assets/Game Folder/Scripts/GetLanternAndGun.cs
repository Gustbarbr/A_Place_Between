using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLanternAndGun : MonoBehaviour
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
            player.lanternInInventory = true;
            player.gunInInventory = true;
        }
    }
}

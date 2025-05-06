using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmuletOfHPIncrease : MonoBehaviour
{
    PlayerControl player;

    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            player.AmuletOfHPIncrease = true;
            Destroy(this.gameObject);
        }
    }
}

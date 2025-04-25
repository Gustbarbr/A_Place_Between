using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmuletOfFLCostReduction : MonoBehaviour
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
            player.AmuletOfFLCostReduction = true;
            Destroy(this.gameObject);
        }
    }
}

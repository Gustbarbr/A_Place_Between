using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    EnemyControl enemy;
    PlayerControl player;

    void Starts(){
        enemy = GetComponent<EnemyControl>();
        player = FindObjectOfType<PlayerControl>();
    }

    void Update() {
        if(enemy.hp <= 0){
           // player.SacredAmulet1 = true;
        }
    }
}

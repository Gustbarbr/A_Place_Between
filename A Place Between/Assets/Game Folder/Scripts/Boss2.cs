using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    EnemyControl enemy;
    PlayerControl player;

    void Start(){
        enemy = GetComponent<EnemyControl>();
        player = FindObjectOfType<PlayerControl>();
    }

    void Update() {
        if(enemy.hp <= 0){
            player.SacredAmulet2 = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnEnemy : MonoBehaviour
{
    EnemyControl enemy;
    void Start()
    {
        enemy = GetComponent<EnemyControl>();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Bullet"){
            if(enemy.hp > 1){
                enemy.hp -= 1;
            }
            else if(enemy.hp <= 1){
                Destroy(this.gameObject);
            }
        }
    }
}

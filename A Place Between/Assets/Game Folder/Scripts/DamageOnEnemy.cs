using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnEnemy : MonoBehaviour
{
    EnemyControl enemy;
    [HideInInspector]
    public float damage = 1;
    void Start()
    {
        enemy = GetComponent<EnemyControl>();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Bullet"){
            if(enemy.hp > damage){
                enemy.hp -= damage;
            }
            else if(enemy.hp <= damage){
                Destroy(this.gameObject);
            }
        }
    }
}

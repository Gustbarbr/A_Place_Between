using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    EnemyControl enemy;

    void Start() {
        enemy = GetComponent<EnemyControl>();
    }

    void Update() {
        if (enemy.hp <= 0)
            SceneManager.LoadScene("Final");
    }
}

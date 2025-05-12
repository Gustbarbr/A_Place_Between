using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScene : MonoBehaviour
{
    private Scene currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        if (currentScene.name == "Death" && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}

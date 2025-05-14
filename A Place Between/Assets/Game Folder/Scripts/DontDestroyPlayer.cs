using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyPlayer : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Garante que o objeto persista entre cenas
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Escuta o carregamento de cenas
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Evita vazamento de eventos
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Destroi o jogador se a cena atual for Menu ou Death
        if (scene.name == "Menu" || scene.name == "Death" || scene.name == "Opening")
        {
            Destroy(gameObject);
        }
    }
}

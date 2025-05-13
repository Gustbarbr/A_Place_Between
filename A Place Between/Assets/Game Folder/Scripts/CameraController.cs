using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{

    public Transform followPlayer; // Guarda o objeto do player

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
        if (scene.name == "Menu" || scene.name == "Death")
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        // A posicao da camera eh igual a do player
        transform.position = new Vector3(followPlayer.position.x, followPlayer.position.y, transform.position.z);
    }
}

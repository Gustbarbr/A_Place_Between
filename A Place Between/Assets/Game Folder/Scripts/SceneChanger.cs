using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    PlayerControl player;
    public Transform spawnPoint;

    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        spawnPoint = GameObject.Find("SpawnPoint")?.transform;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && SceneManager.GetActiveScene().name == "Hospital")
        {
            SceneManager.LoadScene("Outside");
        }

        else if (collider.CompareTag("Player") && SceneManager.GetActiveScene().name == "Outside")
        {
            SceneManager.LoadScene("Floresta");
        }

        else if (collider.CompareTag("Player") && SceneManager.GetActiveScene().name == "Floresta")
        {
            SceneManager.LoadScene("Boss2");
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (spawnPoint != null)
        {
            player.transform.position = spawnPoint.position;
        }
    }
}

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

        if (SceneManager.GetActiveScene().name == "Outside" && player != null && spawnPoint != null)
        {
            player.transform.position = spawnPoint.position;
            Debug.Log("Player movido para o SpawnPoint");
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && SceneManager.GetActiveScene().name == "Hospital" )
        {
            SceneManager.LoadScene("Viatura");
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
}

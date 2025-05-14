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
        //<ALAN> CODIGO ADICIONADO PARA FINS DE TESTE
        //TODO APAGAR 
        else if (Input.GetKeyDown("o"))
        {
            SceneManager.LoadScene("Outside");
        }

        else if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene("Floresta");
        }

        else if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene("Boss2");
        }
        //</ALAN>

    }
}

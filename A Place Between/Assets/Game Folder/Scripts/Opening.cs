using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{
    public float duration = 18f;           // Duração da cutscene em segundos
    private float startTime;

    void Start()
    {
        startTime = Time.time;             // Marca o tempo de início
    }

    void Update()
    {
        float elapsed = Time.time - startTime;
        print(elapsed);
        if (elapsed >= duration || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Hospital");
        }
        
    }
}

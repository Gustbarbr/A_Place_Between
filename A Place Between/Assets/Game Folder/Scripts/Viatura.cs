using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Viatura : MonoBehaviour
{
    public float duration = 7f;           // Duração da cutscene em segundos
    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        float elapsed = Time.time - startTime;
        print(elapsed);
        if (elapsed >= duration || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Outside");
        }
        
    }
}

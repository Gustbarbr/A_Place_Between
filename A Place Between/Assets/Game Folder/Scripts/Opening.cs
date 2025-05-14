using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{
    public float duration = 18f;           // Duração da cutscene em segundos
    private float startTime;

    void Start()
    {
        GameObject mainCamera = GameObject.Find("Main Camera");
        GameObject.Destroy(mainCamera);
        startTime = Time.time;
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

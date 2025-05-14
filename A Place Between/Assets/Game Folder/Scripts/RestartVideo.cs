using UnityEngine;
using UnityEngine.Video;

public class VideoResetOnSceneLoad : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        // Garante que o vídeo comece do início
        videoPlayer.Stop();
        videoPlayer.time = 0;
        videoPlayer.Play();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoSceneController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextScene;
    public TransisiLokal transisi;

    void Start()
    {
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer vp)
    {
        if (transisi != null)
        {
            transisi.PindahScene(nextScene);
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= EndReached;
        }
    }
}
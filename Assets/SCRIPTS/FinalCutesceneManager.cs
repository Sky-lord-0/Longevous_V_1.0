using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class FinalCutsceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string creditsScene = "Credits";

    void Start()
    {
        videoPlayer.loopPointReached += EndVideo;
    }

    void EndVideo(VideoPlayer vp)
    {
        SceneManager.LoadScene(creditsScene);
    }
}
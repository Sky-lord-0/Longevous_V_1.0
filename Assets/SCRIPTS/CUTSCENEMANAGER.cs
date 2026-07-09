using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextScene = "Fase1";
    public float startDelay = 1f;

    IEnumerator Start()
    {
        videoPlayer.playOnAwake = false;

        videoPlayer.loopPointReached += EndVideo;

        yield return new WaitForSeconds(startDelay);

        videoPlayer.Play();
    }

    void EndVideo(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextScene);
    }
}
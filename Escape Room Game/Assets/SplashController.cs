using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(VideoPlayer))]
public class SplashController : MonoBehaviour {

    public VideoPlayer videoPlayer;


	// Use this for initialization
	void Start () {
        videoPlayer = GetComponent<VideoPlayer>();

        videoPlayer.loopPointReached += VideoPlayer_loopPointReached;
	}

    private void VideoPlayer_loopPointReached(VideoPlayer source) {
        SceneManager.LoadScene("MainMenu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class videoPlayManager : MonoBehaviour
{
    public GameObject Sphere;
    VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = Sphere.GetComponent<VideoPlayer>();
    }

    public void PlayButton() {
        videoPlayer.Play();
    }
    public void PauseButton()
    {
        videoPlayer.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

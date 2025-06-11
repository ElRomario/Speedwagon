using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class VideoAndImageSwitch : MonoBehaviour
{
    public GameObject Texts;
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public Image introImage;
    public Image waitingImage;
    private Animator introAnimator;

    void Start()
    {
        introImage.gameObject.SetActive(false);
        waitingImage.gameObject.SetActive(false);
        OnVideoFinished(videoPlayer);
        //videoPlayer.loopPointReached += OnVideoFinished;
        introAnimator = introImage.GetComponent<Animator>();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        rawImage.gameObject.SetActive(false);
        introImage.gameObject.SetActive(true);

        if (introAnimator != null)
        {
            introAnimator.Play("IntroAnimation");
            
        }
    }

    
    public void OnIntroAnimationFinished()
    {
        Texts.SetActive(true);
        introImage.gameObject.SetActive(false);
        waitingImage.gameObject.SetActive(true);
    }
}




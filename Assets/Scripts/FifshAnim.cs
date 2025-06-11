using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class FifshAnim : MonoBehaviour
{
    public Image introImage; // ������ �� Intro (������ �����������)
    public Image waitingImage; // ������ �� "Waiting for Start" (������ �����������)
    public void OnIntroAnimationFinished()
    {
        introImage.gameObject.SetActive(false);
        waitingImage.gameObject.SetActive(true);
    }
}

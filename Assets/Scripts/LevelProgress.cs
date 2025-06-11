using UnityEngine;

public class LevelProgress : MonoBehaviour
{

    public AudioSource musicSource;
    public RectTransform progressBarBackground;  
    public RectTransform progressIndicator;      

    private float barHeight;

    void Start()
    {
        barHeight = progressBarBackground.rect.height;
    }

    void Update()
    {
        if (musicSource.isPlaying && musicSource.clip != null)
        {
            float progress = musicSource.time / musicSource.clip.length;
            Vector2 anchoredPos = progressIndicator.anchoredPosition;
            anchoredPos.y = -progress * barHeight;
            progressIndicator.anchoredPosition = anchoredPos;
        }
    }
}

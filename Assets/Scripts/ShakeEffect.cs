using UnityEngine;

public class UIShakeEffect : MonoBehaviour
{
    RectTransform rectTransform;
    Vector2 originalAnchoredPosition;

    float shakeTimeRemaining = 0f;
    float shakeIntensity = 10f;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (shakeTimeRemaining > 0)
        {
            rectTransform.anchoredPosition = originalAnchoredPosition + Random.insideUnitCircle * shakeIntensity;
            shakeTimeRemaining -= Time.deltaTime;

            if (shakeTimeRemaining <= 0f)
            {
                rectTransform.anchoredPosition = originalAnchoredPosition;
            }
        }
    }

    public void StartShake(float duration, float intensity)
    {
        
        originalAnchoredPosition = rectTransform.anchoredPosition;
        shakeTimeRemaining = duration;
        shakeIntensity = intensity;
    }
}
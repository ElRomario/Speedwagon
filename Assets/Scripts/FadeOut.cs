using UnityEngine;
using UnityEngine.UI;

public class TimedFadeOut : MonoBehaviour
{
    public Image image;             
    public float delay = 3f;        
    public float fadeDuration = 1f; 

    private void Start()
    {
        StartCoroutine(FadeAfterDelay());
    }

    private System.Collections.IEnumerator FadeAfterDelay()
    {
        
        yield return new WaitForSeconds(delay);

        float timer = 0f;
        Color startColor = image.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            image.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        image.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
    }
}

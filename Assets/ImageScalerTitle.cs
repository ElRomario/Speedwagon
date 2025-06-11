using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImageScalerTitle : MonoBehaviour
{
    public RectTransform targetRect;
    public Vector2 targetSize = new Vector2(200, 200);
    public float duration = 0.5f;

    private Coroutine scaleCoroutine;

    public void Start()
    {
        StartScaling();
    }

    public void StartScaling()
    {
        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        scaleCoroutine = StartCoroutine(ScaleToSize(targetSize, duration));
    }

    private IEnumerator ScaleToSize(Vector2 newSize, float time)
    {
        Vector2 startSize = targetRect.sizeDelta;
        float elapsed = 0f;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.rolll);
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / time;
            targetRect.sizeDelta = Vector2.Lerp(startSize, newSize, t);
            yield return null;
        }
        
        targetRect.sizeDelta = newSize;
        SceneManager.LoadScene("Airdrome");
    }
}
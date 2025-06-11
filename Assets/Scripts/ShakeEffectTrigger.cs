using UnityEngine;

public class ShakeButtonTrigger : MonoBehaviour
{
    public UIShakeEffect targetShake;

    public float duration = 0.5f;
    public float intensity = 10f;

    public void TriggerShake()
    {
        targetShake.StartShake(duration, intensity);
    }
}
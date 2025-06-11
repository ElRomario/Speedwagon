using UnityEngine;

public class SmoothYRotation : MonoBehaviour
{
    public float rotationSpeed = 90f; 
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private bool rotatingForward = true;
    private float rotationProgress = 0f;

    void Start()
    {
        startRotation = transform.rotation;
        targetRotation = Quaternion.Euler(0, 90f, 0) * startRotation;
    }

    void Update()
    {
        if (rotationProgress < 1f)
        {
            rotationProgress += Time.deltaTime * (rotationSpeed / 90f); 
            transform.rotation = Quaternion.Slerp(
                rotatingForward ? startRotation : targetRotation,
                rotatingForward ? targetRotation : startRotation,
                rotationProgress
            );
        }
        else
        {
            rotationProgress = 0f;
            rotatingForward = !rotatingForward;
        }
    }
}
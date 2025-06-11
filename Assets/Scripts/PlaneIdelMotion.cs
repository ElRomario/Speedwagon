using UnityEngine;

public class PlaneIdleMotion : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationAmplitude = 5f; 
    public float rotationSpeed = 1f;     

    [Header("Position Settings")]
    public float positionAmplitude = 0.2f; 
    public float positionSpeed = 1f;       

    private float startZ;
    private Quaternion startRotation;

    void Start()
    {
        startZ = transform.localPosition.z;
        startRotation = transform.localRotation;
    }

    void Update()
    {
        
        float zRotation = Mathf.Sin(Time.time * rotationSpeed) * rotationAmplitude;
        transform.localRotation = startRotation * Quaternion.Euler(0f, 0f, zRotation);

        
        float zOffset = Mathf.Sin(Time.time * positionSpeed) * positionAmplitude;
        Vector3 newPosition = transform.localPosition;
        newPosition.z = startZ + zOffset;
        transform.localPosition = newPosition;
    }
}
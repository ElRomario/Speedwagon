using UnityEngine;

public class ArmourBarMovement : MonoBehaviour
{
    public RectTransform crosshair;

    void Update()
    {
        transform.position = crosshair.position + new Vector3(0, 20);
    }
}

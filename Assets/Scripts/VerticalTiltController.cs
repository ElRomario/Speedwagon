using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalTiltController : MonoBehaviour
{

    public RectTransform indicator; // ������ �� ���������
    public RectTransform scale;     // ������ �� �����
    public RailPlaneController planeController;
    public RectTransform crosshair;// ������ �� ���������� �������

    public float maxTilt = 1f; // ������������ �������� �������
    public float smoothSpeed = 5f; // �������� �������� �������� ����������

    private float targetPositionY;

    void Update()
    { // ��������� ������� ����� ���, ����� ��� ��������� �� ��������
        scale.position = crosshair.position;

        // �������� ���� �� RailPlaneController
        float tiltInput = planeController.GetVerticalInput();

        // ��������� ������� ���������� �� �����
        float halfHeight = scale.rect.height / 2; // �������� ������ �����
        targetPositionY = Mathf.Clamp(tiltInput * halfHeight / maxTilt, -halfHeight, halfHeight);

        // ������ ���������� ��������� � ������� �������
        Vector3 currentPos = indicator.localPosition;
        currentPos.y = Mathf.Lerp(currentPos.y, targetPositionY, Time.deltaTime * smoothSpeed);
        indicator.localPosition = currentPos;
    }
}

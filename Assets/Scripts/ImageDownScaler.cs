using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageDownScaler : MonoBehaviour
{
    public float delay = 2f;      // �������� ����� ������� ����������
    public float speed = 10f;     // �������� ����������
    private bool hasStarted = false;
    public bool isAnimating = false;
    private float startTime;

    void Start()
    {
        startTime = Time.time + delay; // ��������� ������ �������, ����� ������ �������� ����������
    }

    void Update()
    {
        // ���� ��������� ��������
        if (!hasStarted && Time.time >= startTime)
        {
            hasStarted = true;
        }

        // ���� �������� ��������, ��������� ������
        if (hasStarted)
        {
            if (transform.localScale.x > 0.01f) // ��������� ��������� �����, ����� �������� ������� � ��������� ������
            {
                isAnimating = true;
                float newScale = Mathf.Max(transform.localScale.x - (3f * Time.deltaTime * speed), 0f);
                transform.localScale = new Vector3(newScale, newScale, newScale);
            }
            else
            {
                transform.localScale = Vector3.zero; // ��������� �������� ������
                isAnimating = false; // ��������� ��������
                hasStarted = false;  // ������������� ���������� ����������
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    public float speed = 50f; // �������� �������� ������
    public float pitchSpeed = 100f; // �������� ������� (������ �����/����)
    public float rollSpeed = 100f; // �������� ����� (������ �����/������)
    public float yawSpeed = 50f; // �������� �������� (������� ������ ������������ ���)
    public float stabilizationSpeed = 2f; // �������� ������������ ����� ������������

    private Rigidbody rb;
    private bool shouldStabilize = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // ��������� ����������, ���� ������� ������ ������
    }

    void Update()
    {
        // ���������� �������
        float pitch = Input.GetAxis("Vertical"); // W/S ��� �������
        float roll = Input.GetAxis("Horizontal"); // A/D ��� �������
        float yaw = Input.GetAxis("Yaw"); // �������������� ���
        float throttle = Mathf.Clamp01(Input.GetAxis("Throttle")); // ��������� (0-1)

        // ��������
        Vector3 rotation = new Vector3(pitch * pitchSpeed, yaw * yawSpeed, -roll * rollSpeed) * Time.deltaTime;
        transform.Rotate(rotation);

        // �������� ������
        rb.linearVelocity = transform.forward * speed;

        // ������������ ����� ������������
        if (shouldStabilize)
        {
            Quaternion targetRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * stabilizationSpeed);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                shouldStabilize = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // �������� ������������
        shouldStabilize = true;

        // ������������� ��������� ��������
        rb.angularVelocity = Vector3.zero;
    }
}

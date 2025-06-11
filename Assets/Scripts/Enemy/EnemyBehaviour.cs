using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    private Vector3 zoneCenter; // ����� ����
    private Vector3 zoneSize; // ������ ����
    private float escapeSpeed;
    private Transform player;// �������� �����

    private bool isEscaping = false;
    private Vector3 escapeDirection;// ���� �����

    public void Initialize(Vector3 center, Vector3 size, float speed, Transform playerTransform)
    {
        zoneCenter = center;
        zoneSize = size;
        escapeSpeed = speed;
        player = playerTransform;
    }

    void Update()
    {
        if (!isEscaping)
        {
            CheckZone();
        }
        else
        {
            Escape();
        }
    }

    void CheckZone()
    {
        // ���������, ��������� �� ���� � �������� ����
        Vector3 halfSize = zoneSize / 2;
        if (transform.position.x < zoneCenter.x - halfSize.x || transform.position.x > zoneCenter.x + halfSize.x ||
            transform.position.y < zoneCenter.y - halfSize.y || transform.position.y > zoneCenter.y + halfSize.y ||
            transform.position.z < zoneCenter.z - halfSize.z || transform.position.z > zoneCenter.z + halfSize.z)
        {
            StartEscape();
        }
    }

    void StartEscape()
    {
        isEscaping = true;

        // ��������� ��������������
       

        // ��������� ����������� ����� � ������ �� ��� Z
        escapeDirection = (player.position - transform.position).normalized;
        escapeDirection.x = 0; // ������� �������� �� X
        escapeDirection.y = 0; // ������� �������� �� Y
    }


    void Escape()
    {
        // ������� ����� � ������� ������ �� ��� Z
        transform.position += escapeDirection * escapeSpeed * Time.deltaTime;

        // ���������� �����, ���� �� ��������� ������
        float distanceToPlayer = Mathf.Abs(transform.position.z - player.position.z);
        if (distanceToPlayer < 1f) // ������� ��� ������������
        {
            Destroy(gameObject); // ���������� ������ (��� ���������� � ��� ��������)
        }
    }
}

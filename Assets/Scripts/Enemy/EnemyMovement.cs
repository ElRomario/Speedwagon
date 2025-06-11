using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Transform[] waypoints; // ����� ���� ��� ��������
    private GameObject player; // ������ �� ������
    public float speed = 5f; // ������� �������� �������� �����
    public float maxDistanceZFromPlayer = 10f; // ������������ ���������� �� ��� Z �� ������

    private int currentWaypointIndex = 0; // ������� ����� ����
    private Vector3 currentSpeed; // ������� �������� ��������
    private Vector3 lastPlayerPosition; // ��������� ������� ������
    private float playerSpeed; // �������� ������

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentSpeed = Vector3.zero;
        lastPlayerPosition = player.transform.position;
    }

    void Update()
    {
        if (waypoints.Length > 0 && player != null)
        {
            CalculatePlayerSpeed();
            MoveEnemy();
        }
    }

    // ���������� �������� ������ �� ������� � ��������
    void CalculatePlayerSpeed()
    {
        // ������� � ������� ������ ����� �������
        Vector3 playerVelocity = (player.transform.position - lastPlayerPosition) / Time.deltaTime;
        playerSpeed = playerVelocity.magnitude; // �������� �������� �������� ������

        // ��������� ��������� ������� ������ ��� ���������� �����
        lastPlayerPosition = player.transform.position;
    }

    void MoveEnemy()
    {
        // �������� ������� � ������� �������
        Vector3 currentPosition = transform.position;
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // ������������ ����������� �������� � ������� ����� ����
        Vector3 directionToWaypoint = (targetWaypoint.position - currentPosition).normalized;

        // ��������� ���������� �� ��� Z �� ������
        float distanceZToPlayer = Mathf.Abs(currentPosition.z - player.transform.position.z);

        if (distanceZToPlayer > maxDistanceZFromPlayer)
        {
            // ���� ���� ������� �� �������, �������� ����������� ��������
            float clampedZ = Mathf.Clamp(
                currentPosition.z,
                player.transform.position.z - maxDistanceZFromPlayer,
                player.transform.position.z + maxDistanceZFromPlayer
            );

            // ����� �������� ���������� ����� ������� � ���������� �������
            Vector3 targetPosition = new Vector3(currentPosition.x, currentPosition.y, clampedZ);
            Vector3 correctionDirection = (targetPosition - currentPosition).normalized;

            currentSpeed = correctionDirection * playerSpeed; // �������� ����� = �������� ������
        }
        else
        {
            // ���� ���� � �������� ���������� ���������, ���������� ��� � ����� ����
            currentSpeed = directionToWaypoint * speed;
        }

        // ��������� ������� �����
        transform.position += currentSpeed * Time.deltaTime;

        // ���� �������� ������� ����� ����, ��������� � ���������
        if (Vector3.Distance(currentPosition, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; // ����������� ����
        }
    }
}


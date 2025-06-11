using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupMover : MonoBehaviour
{

    private Transform[] waypoints; // ����� ����
    private float moveToPathSpeed; // �������� ����������� � ��������� ����� ����
    private float pathSpeed; // �������� �������� �� ����
    private bool movingToPath = false; // ���� �������� � ����
    private bool movingOnPath = false; // ���� �������� �� ����
    private int currentWaypointIndex = 0; // ������ ������� ����� ����

    public void Initialize(Transform[] waypoints, float moveToPathSpeed, float pathSpeed)
    {
        this.waypoints = waypoints;
        this.moveToPathSpeed = moveToPathSpeed;
        this.pathSpeed = pathSpeed;

        if (waypoints != null && waypoints.Length > 0)
        {
            movingToPath = true; // �������� �������� � ������ ����� ����
        }
    }

    void Update()
    {
        if (movingToPath)
        {
            MoveToPath();
        }
        else if (movingOnPath)
        {
            MoveAlongPath();
        }
    }

    private void MoveToPath()
    {
        // ��������� � ������ ����� ����
        Transform targetWaypoint = waypoints[0];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveToPathSpeed * Time.deltaTime);

        // ���� �������� ������ ����� ����, ��������� � �������� �� ����
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            movingToPath = false;
            movingOnPath = true;
        }
    }

    private void MoveAlongPath()
    {
        // ��������� �� ������ ����
        if (waypoints == null || waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, pathSpeed * Time.deltaTime);

        // ���� �������� ������� ����� ����, ������������� �� ���������
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0; // ����������� �������� �� ����
            }
        }
    }
}

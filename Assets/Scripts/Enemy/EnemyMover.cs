using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private Transform[] waypoints; // ������ ����� ����
    private float moveToPathSpeed; // �������� �������� � ��������� ����� ����
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
            movingToPath = true; // �������� �������� � ��������� ����� ����
            currentWaypointIndex = FindClosestWaypointIndex();
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

    private int FindClosestWaypointIndex()
    {
        // ������� ������ ��������� ����� ����
        int closestIndex = 0;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < waypoints.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, waypoints[i].position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        return closestIndex;
    }

    private void MoveToPath()
    {
        // ��������� � ��������� ����� ����
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveToPathSpeed * Time.deltaTime);

        // ���� �������� ��������� ����� ����, ������������� �� �������� �� ����
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            movingToPath = false;
            movingOnPath = true;
        }
    }

    private void MoveAlongPath()
    {
        // ��������� �� ������ ����
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

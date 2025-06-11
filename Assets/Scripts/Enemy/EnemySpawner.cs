using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // ������ �����
    public Transform[] spawnPoints; // ����� ������
    public Path[] possiblePaths; // ������ ����� (������ �������� ������ Path)

    private void Start()
    {
        // �������� ��� �������� ������� �������, � �������� �������� EnemySpawner (SpawnPoints)
        spawnPoints = GetComponentsInChildren<Transform>();
        spawnPoints = System.Array.FindAll(spawnPoints, point => point != transform); // ������� ��� ������

        // �������� ����� ������
        InvokeRepeating(nameof(SpawnEnemy), 0f, 2f);
    }

    void SpawnEnemy()
    {
        // ��������� ������� �������� ����� ��� ������
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // ��������� ������� �������� ���� �� �����
        Path selectedPath = possiblePaths[Random.Range(0, possiblePaths.Length)];

        // ������ ����� � ��������� �����
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        // ������� ���� ����� ��� ��� ��������, ������� � ����� ��������� � ������
        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            // ������� ��������� ����� ���� � ������
            Transform startPoint = FindNearestWaypoint(selectedPath.waypoints, spawnPoint.position);
            // �������� ���� ������� � ���� �����
            Transform[] newPath = GetPathFromStartPoint(selectedPath.waypoints, startPoint);

            // �������� ����� ���� �����
            enemyMovement.waypoints = newPath;
        }

        Debug.Log($"Spawned enemy at: {spawnPoint.position}, Path: {selectedPath.waypoints.Length} waypoints");
    }

    // ����� ��� ������ ��������� ����� ���� � ����� ������
    Transform FindNearestWaypoint(Transform[] waypoints, Vector3 spawnPosition)
    {
        Transform nearestWaypoint = waypoints[0];
        float nearestDistance = Vector3.Distance(spawnPosition, nearestWaypoint.position);

        foreach (var waypoint in waypoints)
        {
            float distance = Vector3.Distance(spawnPosition, waypoint.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestWaypoint = waypoint;
            }
        }

        return nearestWaypoint;
    }

    // ����� ��� ��������� ���� ������� � �����, ������� �������� � ������
    Transform[] GetPathFromStartPoint(Transform[] waypoints, Transform startPoint)
    {
        int startIndex = System.Array.IndexOf(waypoints, startPoint);
        Transform[] newPath = new Transform[waypoints.Length - startIndex];
        System.Array.Copy(waypoints, startIndex, newPath, 0, newPath.Length);
        return newPath;
    }

    // ���� ����� ����� ���������� ��� ����������� ����� � ���������
    private void OnDrawGizmos()
    {
        if (possiblePaths == null) return;

        // ������� �� ���� ����� � �������� ��
        foreach (var path in possiblePaths)
        {
            if (path != null)
            {
                path.DrawPath(); // ������ ���� ��� ������� ������� Path
            }
        }
    }
}

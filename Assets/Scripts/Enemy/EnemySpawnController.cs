using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // ������ �����
    public RailPlaneController planeController;
    public Transform player;// ������ �� ������ ���������� ��������

    public float zoneMinZ = -10f;
    public float zoneMaxZ = 30f;
    public float zoneX = 20f;
    public float zoneY = 10f;
    public float escapeSpeed = 20f;

    public GameObject enemyPrefab;   // ������ �����
    public GameObject bulletPrefab; // ������ ����
    public Transform[] spawnPoints; // ����� ������ ������
    public Transform[] shootPoints; // ����� ��������

    [Header("��������� �������� ������")]
    public float fireRate = 2f;      // �������� ��������
    public float bulletSpeed = 15f; // �������� ����
    public float spread = 5f;       // ������� ����

    public Vector3 zoneCenter = new Vector3(0, 0, 20f); // ����� ����
    public Vector3 zoneSize = new Vector3(40f, 20f, 40f); // ������ ���� (������, ������, �������)
    // ���� ���� ��� ������������
    public Color zoneColor = new Color(0f, 1f, 0f, 0.2f);

    public Transform[] waypoints; // ������ ����� ����
    public float moveToPathSpeed = 5f; // �������� �������� � ��������� ����� ����
    public float pathSpeed = 3f; // �������� �������� �� ����


    [Header("Spawn Settings")]
    public float zOffset = 5f; // �������� �� ��� Z ��� ������ ������
    public float maxRandomOffset = 2f; // ������������ ��������� �������� ������������ ����
    
    Quaternion spawnRotation = Quaternion.Euler(180,0, 0);
    private bool hasTriggered = false; // ���� ��� ������������ ������������ ��������

    void OnTriggerEnter(Collider other)
    {
        // ���������, ��� ������� ������ �����
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.enemySpawn);
            // ������������ ����� ������ � �������� �����
            Vector3 spawnPosition = CalculateRandomSpawnPosition();

            GameObject selectedPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // ������� �����
            GameObject enemy = Instantiate(selectedPrefab, spawnPosition, spawnRotation);

            // �������� ����� ��� �������� �� ����
            EnemyMover enemyMover = enemy.GetComponent<EnemyMover>();
            if (enemyMover != null)
            {
                // �������������� �����, ����� �� ����� ��������� �� ���� � ������ ��������
                enemyMover.Initialize(waypoints, moveToPathSpeed, pathSpeed);
            }
            else
            {
                Debug.Log("Enemy Mover is Null!");
            }

            EnemyShooting enemyShooting = enemy.GetComponentInChildren<EnemyShooting>();
            if (enemyShooting != null)
            {
                enemyShooting.SetupShooting(
                    enemyShooting.shootPoint, // shootPoint ��� ����� � �������
                    bulletPrefab,
                    fireRate,
                    bulletSpeed,
                    spread
                 
                );
                // �������� ��������� �����
                EnemyBehaviour enemyBehavior = enemy.GetComponent<EnemyBehaviour>();
                if (enemyBehavior != null)
                {
                    enemyBehavior.Initialize(zoneCenter, zoneSize, escapeSpeed, player);
                }

            }
        }
    }


        // ������������ ����� ������ � ������ ��������
        Vector3 CalculateRandomSpawnPosition()
        {
            // ������� ��������� ����� ���� � ������� �����
            Transform closestWaypoint = GetClosestWaypoint();

            // �������� ������ ��������� ����� ����
            int nextWaypointIndex = (planeController.currentWaypointIndex + 1) % waypoints.Length;
            Transform nextWaypoint = waypoints[nextWaypointIndex];

            // ������������ ����������� �������� ����� ������� � ��������� ������ ����
            Vector3 direction = (nextWaypoint.position - closestWaypoint.position).normalized;

            // ��������� �������� ������������ ���� �� ���� X � Y
            float randomOffsetX = Random.Range(-maxRandomOffset, maxRandomOffset);
            float randomOffsetY = Random.Range(-maxRandomOffset, maxRandomOffset);

            // ��������� �������� ����� ����
            Vector3 offset = direction * zOffset;
            Vector3 spawnPosition = closestWaypoint.position + offset;
            spawnPosition.x += randomOffsetX;
            spawnPosition.y += randomOffsetY;

            return spawnPosition;
        }

        // ������� ��� ������ ��������� ����� ����
        Transform GetClosestWaypoint()
        {
            Transform closest = waypoints[0];
            float minDistance = Vector3.Distance(planeController.boundsCenter, closest.position);

            foreach (var waypoint in waypoints)
            {
                float distance = Vector3.Distance(planeController.boundsCenter, waypoint.position);
                if (distance < minDistance)
                {
                    closest = waypoint;
                    minDistance = distance;
                }
            }

            return closest;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = zoneColor;

            // ������ �������������� ���
            Gizmos.DrawCube(zoneCenter, zoneSize);

            // ������ ����
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(zoneCenter, zoneSize);
        }
}
        
         

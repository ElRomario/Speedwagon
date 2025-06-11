using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{

    public GameObject enemyGroupPrefab; // ������ ������ ������
    public Transform spawnPoint; // ����� ������ ������ ������
    public float spawnDelay = 0f; // �������� ����� ������� ������ ������

    private bool hasTriggered = false; // ����, ����� ��������� ���� ���

    void OnTriggerEnter(Collider other)
    {
        // ���������, ��� ������� ������ �����
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            Invoke("SpawnEnemyGroup", spawnDelay);
        }
    }

    void SpawnEnemyGroup()
    {
        // ������ ������ ������ � ��������� �����
        if (enemyGroupPrefab != null && spawnPoint != null)
        {
            Instantiate(enemyGroupPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}

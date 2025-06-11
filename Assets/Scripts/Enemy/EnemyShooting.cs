using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("��������� ��������")]
    public GameObject bulletPrefab;  // ������ ����
    public Transform shootPoint;     // ����� ������ ����
    private float fireRate = 1f;      // �������� �������� (���� � �������)
    private float bulletSpeed = 10f;  // �������� ����� ����
    private float spread = 1f;        // ������� ��������� (� ��������)

    private Transform player;        // ������ �� ������
    private float nextFireTime = 0f;

    public void SetupShooting(Transform newShootPoint, GameObject newBulletPrefab, float newFireRate, float newBulletSpeed, float newSpread)
    {
        // ���������, ��� �� ������� ����� shootPoint
        if (newShootPoint != null)
        {
            shootPoint = newShootPoint;
        }

        bulletPrefab = newBulletPrefab;
        fireRate = newFireRate;
        bulletSpeed = newBulletSpeed;
        spread = newSpread;
    }

    void Start()
    {
        // ������� ������ �� ����
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        // �������� � �������� ��������
        if (Time.time >= nextFireTime)
        {
            ShootAtPlayer();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void ShootAtPlayer()
    {
        if (player == null || shootPoint == null || bulletPrefab == null)
        {
            Debug.LogWarning("��������� ��� �������� �� ������ ���������.");
            return;
        }

        // ��������� ����������� �� ������
        Vector3 directionToPlayer = (player.position - shootPoint.position).normalized;

        // ��������� �������
        directionToPlayer += new Vector3(
            Random.Range(-spread, spread) * 0.01f,
            Random.Range(-spread, spread) * 0.01f,
            Random.Range(-spread, spread) * 0.01f
        );

        // ������ ����
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(directionToPlayer));

        // ������� � ���� ��������� ��������
        BulletMovement bulletScript = bullet.GetComponent<BulletMovement>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(directionToPlayer, bulletSpeed);
        }

        // ���������� ���� ����� 5 ������
        Destroy(bullet, 5f);
    }
    void OnDrawGizmos()
    {
        if (shootPoint != null && player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(shootPoint.position, player.position);
        }
    }
}

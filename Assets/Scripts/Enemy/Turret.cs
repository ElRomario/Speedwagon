using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject missilePrefab; // ������ ������
    public Transform missileSpawnPoint; // ����� ������ ������
    public Transform turretHead; // ����� ������, ������� ��������������
    public Transform player; // ���� � ������ ������
    public float fireRate = 3f; // �������� ��������
    public float rotationSpeed = 5f; // �������� �������� ������

    private float nextFireTime = 0f;

    void Update()
    {
        if (player != null)
        {
            RotateToPlayer();
            FireMissileIfReady();
        }
    }

    void RotateToPlayer()
    {
        // ���������� ������ � ������� ������
        Vector3 direction = (player.position - turretHead.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        turretHead.rotation = Quaternion.Slerp(turretHead.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void FireMissileIfReady()
    {
        // ���������, ����� �� ������ ��������
        if (Time.time >= nextFireTime)
        {
            FireMissile();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FireMissile()
    {
        //// ������ ������ � ����� �� ����
        //GameObject missile = Instantiate(missilePrefab, missileSpawnPoint.position, missileSpawnPoint.rotation);
        //HomingMissile homingMissile = missile.GetComponent<HomingMissile>();

        //if (homingMissile != null && player != null)
        //{
        //    homingMissile.SetTarget(player); // ������� ���� ������
        //}
    }
}

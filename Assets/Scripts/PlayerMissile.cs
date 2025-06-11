using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : Missile
{
    public GameObject explosion;
    protected override void Start()
    {
        // ��� ��������� ����� ������������� � ���������� ��� ������ ������
        dropTime = 0.05f; // ����� ������� ������ ������
        speed = 250f; // �������� ������ ������
        turnSpeed = 7f; // �������� �������� ������ ������

        base.Start();
    }

    protected override void OnHitTarget(GameObject target)
    {
        AudioManager.Instance.PlayExplosions();
        // ������������� ��������� ��� ��������� ��� ������ ������
        Debug.Log("Player missile hit " + target.name);
        Instantiate(explosion, target.transform.position, Random.rotationUniform);
        base.OnHitTarget(target);
    }
}
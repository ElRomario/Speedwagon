using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileShooter : MissileShooter
{
    public float firingRange = 20f;// ��������� ��������
    public float disarmRange = 100f;
    private GameObject target; // ���� ����� (��������, �����)


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    protected override List<GameObject> GetTargets()
    {
        // ���������, ��������� �� ����� � ���� ���������
        if (target != null && Vector3.Distance(transform.position, target.transform.position) <= firingRange)
        {
            return new List<GameObject> { target.gameObject };
        }

        return new List<GameObject>();
    }

    void Update()
    {
        
        FireMissiles();
    }
}

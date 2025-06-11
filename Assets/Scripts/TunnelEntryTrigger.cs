using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelEntryTrigger : MonoBehaviour
{
    public static bool canTakeDamage = false;


    public MeshCollider tunnelCollider; // ������ �� MeshCollider �������

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tunnelCollider.enabled = true; // �������� MeshCollider
            Debug.Log("�������� ������� ��������!");

            // ������������� �������� ����, ���� ����� ��� � �������
            if (tunnelCollider.bounds.Contains(other.transform.position))
            {
                 
                Debug.Log("����� ��� ������ ������� - ������� ����.");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelCollisionManager : MonoBehaviour
{
    public MeshCollider tunnelMeshCollider; // ������ �� MeshCollider �������

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tunnelMeshCollider.enabled = false; // ��������� �������� �������
            Debug.Log("����� ����� � ������� - �������� ���������.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tunnelMeshCollider.enabled = true; // �������� �������� �������
            Debug.Log("����� ������� ������� ������� - �������� ��������.");
        }
        
    }
}

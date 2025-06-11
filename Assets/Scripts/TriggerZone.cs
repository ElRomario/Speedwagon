using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public GameObject objectToActivate; // ������ �� ������, ������� ����� ������������
    public Color gizmoColor = Color.green; // ���� ��� ����������� ����

    private BoxCollider boxCollider;

    private void Awake()
    {
        // �������� ��������� BoxCollider � ����������, ��� �� �������� ���������
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ���������, ��� � ������� ����� �����
        {
            objectToActivate.SetActive(true); // ���������� ������
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // ���������, ��� �� �������� ����� �����
        {
            Destroy(gameObject); // ���������� ������� ������ (�������)
        }
    }

    private void OnDrawGizmos()
    {
        // ����������� ���� � ������ ����� ����������
        Gizmos.color = gizmoColor;

        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider>();

        if (boxCollider != null)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);
        }
    }
}

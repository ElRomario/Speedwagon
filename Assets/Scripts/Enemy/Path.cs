using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform[] waypoints; // ������ ����� ����

    // ���� ����� ������ ���� � ��������� � ������� Gizmos
    public void DrawPath()
    {
        if (waypoints == null || waypoints.Length < 2) return;

        // ������� �� ���� ������ ���� � ������ ����� ����� ����
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            if (waypoints[i] != null && waypoints[i + 1] != null)
            {
                Gizmos.color = Color.red; // ������ ���� ����� (����� ��������)
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position); // ������ ����� ����� �������
            }
        }
    }
}

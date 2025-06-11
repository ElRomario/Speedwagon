using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class EnemyMarker : MonoBehaviour
    {
    public GameObject markerPrefab; // ������ �������
    private GameObject markerInstance;
    private RectTransform canvasRect; // RectTransform Canvas'�

    void Start()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            canvasRect = canvas.GetComponent<RectTransform>();

            // ������� ������ � ��������� ��� � Canvas
            if (markerPrefab != null)
            {
                markerInstance = Instantiate(markerPrefab, canvas.transform);
            }
        }
    }

    void Update()
    {
        if (markerInstance != null)
        {
            // ����������� ������� ���������� ����� � ��������
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

            // ���� ���� � ���� ������
            if (screenPos.z > 0)
            {
                markerInstance.SetActive(true);

                // ����������� �������� ���������� � ���������� UI (Canvas)
                Vector2 uiPosition;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvasRect,
                    screenPos,
                    null, // ������ �� ����� ��� Screen Space - Overlay
                    out uiPosition
                );

                // ��������� ������� �������
                markerInstance.GetComponent<RectTransform>().anchoredPosition = uiPosition;
            }
            else
            {
                // �������� ������, ���� ���� ��� ������
                markerInstance.SetActive(false);
            }
        }
    }

    void OnDestroy()
    {
        // ������� ������ ��� ����������� �����
        if (markerInstance != null)
        {
            Destroy(markerInstance);
        }
    }
}


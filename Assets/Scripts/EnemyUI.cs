using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{

    public GameObject uiPrefab; // ������ ��� Image
    public float visibilityDistance = 50f; // ��������� ��������� (������������� � ����������)

    private GameObject uiElement; // ������ �� ��������� ������� UI
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        // �������� Canvas ����� ��������
        Canvas uiCanvas = UIManager.Instance?.uiCanvas;
        if (uiCanvas != null && uiPrefab != null)
        {
            uiElement = Instantiate(uiPrefab, uiCanvas.transform);
            uiElement.SetActive(false); // �������� �� ��������� �����
        }
    }

    void Update()
    {
        if (uiElement != null)
        {
            // ������������ ������� ���������� � ��������
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position);

            // ���������, ����� �� ���� � ��������� �� � �������� ���������
            float distanceToCamera = Vector3.Distance(transform.position, mainCamera.transform.position);
            if (screenPosition.z > 0 && distanceToCamera <= visibilityDistance)
            {
                uiElement.SetActive(true);
                uiElement.GetComponent<RectTransform>().position = screenPosition;
            }
            else
            {
                uiElement.SetActive(false);
            }
        }
    }

    void OnDestroy()
    {
        // ������� UI-������� ��� ����������� �����
        if (uiElement != null)
        {
            Destroy(uiElement);
        }
    }
}
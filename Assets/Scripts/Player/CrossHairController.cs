using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairController : MonoBehaviour
{


    [SerializeField] private Transform shootPoint; 
    [SerializeField] private RectTransform crosshairUI;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private RailPlaneController planeController;

    public float distance = 100f;

    private Vector3 lockedCrosshairPosition; 
    private Vector3 lockedShootPointPosition; 
    private Quaternion lockedShootPointRotation; 
    private bool isLocked = false; 

    void Start()
    {

        if (shootPoint == null)
        {
            //shootPoint = transform.Find("ShootPoint");
            if (shootPoint == null)
            {
                Debug.LogError("ShootPoint �� ������!");
            }

            else
            {

                Debug.Log("ShootPoint ������");
            }
        }

        if (crosshairUI == null)
        {
            crosshairUI = GameObject.Find("CrosshairUI")?.GetComponent<RectTransform>();
            if (crosshairUI == null) Debug.LogError("CrosshairUI �� ������!");
        }

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null) Debug.LogError("Main camera �� ������!");
        }

        if (planeController == null)
        {
            planeController = FindObjectOfType<RailPlaneController>();
            if (planeController == null) Debug.LogError("PlaneController �� ������!");
        }
    }

    void Update()
    {
        if (crosshairUI == null || mainCamera == null || planeController == null)
        {
            Debug.LogWarning("�� ��� ������ ����������� � CrossHairController!");
            return;
        }

        
        if (planeController.isBarrelRolling && !isLocked)
        {
            LockCrosshairAndShootPoint();
        }

        
        if (!planeController.isBarrelRolling && isLocked)
        {
            UnlockCrosshairAndShootPoint();
        }

        // ������������ ������� �������
        if (isLocked)
        {
            // �� ����� ����� ���������� ��������������� ��������
            Vector3 targetPoint = lockedShootPointPosition + lockedShootPointRotation * Vector3.forward * distance;
            Vector3 screenPoint = mainCamera.WorldToScreenPoint(targetPoint);
            crosshairUI.position = lockedCrosshairPosition;
        }
        else
        {
            // ��� �������� ��������� ������
            Vector3 targetPoint = shootPoint.position + shootPoint.forward * distance;
            Vector3 screenPoint = mainCamera.WorldToScreenPoint(targetPoint);
            crosshairUI.position = screenPoint;
        }
    }

    private void LockCrosshairAndShootPoint()
    {
        lockedCrosshairPosition = crosshairUI.position; // ��������� ������� �������
        lockedShootPointPosition = shootPoint.position; // ��������� ������� shootPoint
        lockedShootPointRotation = shootPoint.rotation; // ��������� ���������� shootPoint
        isLocked = true; // ������������� ���� ��������
    }

    private void UnlockCrosshairAndShootPoint()
    {
        isLocked = false; // ���������� ��������
    }
}

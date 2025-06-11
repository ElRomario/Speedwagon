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
                Debug.LogError("ShootPoint не найден!");
            }

            else
            {

                Debug.Log("ShootPoint найден");
            }
        }

        if (crosshairUI == null)
        {
            crosshairUI = GameObject.Find("CrosshairUI")?.GetComponent<RectTransform>();
            if (crosshairUI == null) Debug.LogError("CrosshairUI не найден!");
        }

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null) Debug.LogError("Main camera не найден!");
        }

        if (planeController == null)
        {
            planeController = FindObjectOfType<RailPlaneController>();
            if (planeController == null) Debug.LogError("PlaneController не найден!");
        }
    }

    void Update()
    {
        if (crosshairUI == null || mainCamera == null || planeController == null)
        {
            Debug.LogWarning("Не все ссылки установлены в CrossHairController!");
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

        // Рассчитываем позицию прицела
        if (isLocked)
        {
            // Во время бочки используем зафиксированные значения
            Vector3 targetPoint = lockedShootPointPosition + lockedShootPointRotation * Vector3.forward * distance;
            Vector3 screenPoint = mainCamera.WorldToScreenPoint(targetPoint);
            crosshairUI.position = lockedCrosshairPosition;
        }
        else
        {
            // Без фиксации обновляем данные
            Vector3 targetPoint = shootPoint.position + shootPoint.forward * distance;
            Vector3 screenPoint = mainCamera.WorldToScreenPoint(targetPoint);
            crosshairUI.position = screenPoint;
        }
    }

    private void LockCrosshairAndShootPoint()
    {
        lockedCrosshairPosition = crosshairUI.position; // Фиксируем позицию прицела
        lockedShootPointPosition = shootPoint.position; // Фиксируем позицию shootPoint
        lockedShootPointRotation = shootPoint.rotation; // Фиксируем ориентацию shootPoint
        isLocked = true; // Устанавливаем флаг фиксации
    }

    private void UnlockCrosshairAndShootPoint()
    {
        isLocked = false; // Сбрасываем фиксацию
    }
}

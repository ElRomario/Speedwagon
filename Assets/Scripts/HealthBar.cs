using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [Header("��������� ��������")]
    public float maxHealth = 100f; // ������������ ��������
    public float currentHealth;

    [Header("UI ��������")]
    public RectTransform healthBarFill; // ������ �� RectTransform �������

    private float originalWidth; // �������� ������ HealthBarFill

    void Start()
    {
        // ������������� ��������� �������� ��������
        currentHealth = maxHealth;

        // ��������� �������� ������ HealthBarFill
        if (healthBarFill != null)
        {
            originalWidth = healthBarFill.sizeDelta.x;
        }
    }

    public void TakeDamage(float damage)
    {
        // ��������� ��������
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // ��������� ������ HealthBarFill
        UpdateHealthBar();

        // ��������� ������
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            // ������������ ����� ������
            float newWidth = (currentHealth / maxHealth) * originalWidth;

            // ������������� ����� ������ HealthBarFill
            healthBarFill.sizeDelta = new Vector2(newWidth, healthBarFill.sizeDelta.y);
        }
    }

    void Die()
    {
        Debug.Log("�������� ����!");
        // ������ ������
    }
}

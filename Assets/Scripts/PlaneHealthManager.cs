using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneHealthManager : MonoBehaviour
{
    private Health health;

    [Header("UI элементы")]
    public RectTransform healthBarFill; 

    private float originalWidth; 

    void Awake()
    {
        health = GetComponent<Health>();

        
        health.OnHealthChanged += UpdateUI;
        health.OnDeath += HandleDeath;

        
        if (healthBarFill != null)
        {
            originalWidth = healthBarFill.sizeDelta.x;
        }
    }

    void UpdateUI(float current, float max)
    {
        
        if (healthBarFill == null) return;

        
        float newWidth = (current / max) * originalWidth;

        
        healthBarFill.sizeDelta = new Vector2(newWidth, healthBarFill.sizeDelta.y);
    }

    void HandleDeath()
    {
        Debug.Log("DEAD");
    }
}

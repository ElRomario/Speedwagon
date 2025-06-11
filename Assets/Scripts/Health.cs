using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Health;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public delegate void HealthChanged(float current, float max);
    public event HealthChanged OnHealthChanged; 

    public delegate void Death();
    public event Death OnDeath; 
    public GameObject explosion;
    public SceneReloader sceneReloader;

    [SerializeField]private Armor armor;
    [SerializeField]private PlaneArmorManager armorManager;
    [SerializeField]private GameObject armourBackground;


    void Start()
    {
        if (GameManager.Instance.SelectedPlaneName == "Su47")
        {
            maxHealth = 300;
        }
        else
        {
            maxHealth = 150;
        }
        currentHealth = maxHealth;

        if (GameManager.Instance.armourSelected)
        {
            armourBackground.SetActive(true);
            armor.enabled = true;
            armorManager.enabled = true;
        
        }
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0) return;

        if (armor.enabled && armor.CurrentArmor > 0)
        {
            float leftover = damage - armor.CurrentArmor;
            armor.TakeDamage(damage);

            if (leftover > 0)
                currentHealth -= leftover;
        }
        else
        {
            currentHealth -= damage;
        }
    
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    //public void Heal(float amount)
    //{
    //    currentHealth += amount;
    //    currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

    //    OnHealthChanged?.Invoke(currentHealth, maxHealth);
    //}

    void Die()
    {
        OnDeath?.Invoke();
        AudioManager.Instance.PlayExplosions();
        Instantiate(explosion, transform.position, Random.rotationUniform);
        Destroy(gameObject); 
    }
}

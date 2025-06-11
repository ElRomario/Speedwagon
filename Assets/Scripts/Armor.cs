using System;
using UnityEngine;

public class Armor : MonoBehaviour
{
    public float MaxArmor = 100f;
    public float CurrentArmor { get; private set; }

    public event Action<float, float> OnArmorChanged;

    void Awake()
    {
        CurrentArmor = MaxArmor;
    }

    public void TakeDamage(float amount)
    {
        CurrentArmor -= amount;
        if (CurrentArmor < 0)
            CurrentArmor = 0;

        OnArmorChanged?.Invoke(CurrentArmor, MaxArmor);
    }

    public void RestoreArmor(float amount)
    {
        CurrentArmor = Mathf.Min(CurrentArmor + amount, MaxArmor);
        OnArmorChanged?.Invoke(CurrentArmor, MaxArmor);
    }
}

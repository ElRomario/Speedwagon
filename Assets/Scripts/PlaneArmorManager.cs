using UnityEngine;

public class PlaneArmorManager : MonoBehaviour
{
    private Armor armor;

    [Header("UI элементы")]
    public RectTransform armorBarFill;

    private float originalWidth;

    void Awake()
    {
        armor = GetComponent<Armor>();

        armor.OnArmorChanged += UpdateUI;

        if (armorBarFill != null)
        {
            originalWidth = armorBarFill.sizeDelta.x;
        }
    }

    void UpdateUI(float current, float max)
    {
        if (armorBarFill == null) return;

        float newWidth = (current / max) * originalWidth;
        armorBarFill.sizeDelta = new Vector2(newWidth, armorBarFill.sizeDelta.y);
    }
}

using UnityEngine;
using TMPro;

public class BonusCardsManager : MonoBehaviour
{
    [SerializeField] GameObject PlaneCard;
    [SerializeField] GameObject ArmourCard;
    [SerializeField] GameObject MissilesCard;

    [SerializeField] GameObject PlaneCardBan;
    [SerializeField] GameObject ArmourCardBan;
    [SerializeField] GameObject MissilesCardBan;

    [SerializeField] GameObject PlaneButton;


    private void OnDisable()
    {
        GameManager.Instance.OnPlaneCollected -= HandlePlaneCollected;
        GameManager.Instance.OnArmourCollected -= HandleArmourCollected;
        GameManager.Instance.OnMissilesCollected -= HandleMissileCollected;
    }

    private void Start()
    {
        GameManager.Instance.OnPlaneCollected += HandlePlaneCollected;
        GameManager.Instance.OnArmourCollected += HandleArmourCollected;
        GameManager.Instance.OnMissilesCollected += HandleMissileCollected;

        if (GameManager.Instance.SelectedPlaneName == "Su47")
        {
            PlaneCardBan.SetActive(true);
        }
        if (GameManager.Instance.armourSelected)
        {
            ArmourCardBan.SetActive(true);
        };
        if (GameManager.Instance.missilesSelected)
        { 
            MissilesCardBan.SetActive(true);
        };
    }

    private void HandlePlaneCollected()
    {
        PlaneCard.SetActive(true);
    }
    private void HandleMissileCollected()
    {
        MissilesCard.SetActive(true);
    }
    private void HandleArmourCollected()
    {
        ArmourCard.SetActive(true);
    }

    public void SelectPlane()
    {
        GameManager.Instance.SetSelectedPlane("Su47");
        PlaneCardBan.SetActive(true);  

    }
    public void SelectMissiles() 
    { 
        GameManager.Instance.SetMissileSelected(true);
        MissilesCardBan.SetActive(true);
        PlayerPrefs.SetInt("missilesFlag", 1);
    }
    public void SelectArmour() 
    {   GameManager.Instance.SetArmourSelected(true);
        ArmourCardBan.SetActive(true);
        PlayerPrefs.SetInt("armourFlag", 1);
    }

}

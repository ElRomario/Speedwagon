using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Выбранный самолёт")]
    public string selectedPlaneName = "Su47";
    [Header("Бонусы")]
    [SerializeField]public bool planeCollected = false;
    [SerializeField] public bool missilesCollected = false;
    [SerializeField] public bool armourCollected = false;

    [SerializeField] public bool planeSelected = false;
    [SerializeField] public bool missilesSelected = false;
    [SerializeField] public bool armourSelected = false;

    public string SelectedPlaneName => selectedPlaneName;

    public event Action OnPlaneCollected; 
    public event Action OnMissilesCollected; 
    public event Action OnArmourCollected; 


    public void setPlaneCollected(bool flag)
    {
        planeCollected = flag;
        OnPlaneCollected?.Invoke();
    }

    public void setMissileCollected(bool flag)
    {
        missilesCollected = flag;
        OnMissilesCollected?.Invoke();
    }
    public void setArmourCollected(bool flag)
    {
        armourCollected = flag;
        OnArmourCollected?.Invoke();
    }

               
    public void SetMissileSelected(bool flag)
    {
        missilesSelected = flag;         
    }            
    public void SetArmourSelected(bool flag)
    {
        armourSelected = flag;
    }
   

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            
            if (PlayerPrefs.HasKey("SelectedPlane"))
            {
                selectedPlaneName = PlayerPrefs.GetString("SelectedPlane");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSelectedPlane(string name)
    {
        selectedPlaneName = name;
        PlayerPrefs.SetString("SelectedPlane", name);
        PlayerPrefs.Save();
    }

    public void LoadSelectedPlane(Transform spawnPoint)
    {
        GameObject planePrefab = Resources.Load<GameObject>("Planes/" + selectedPlaneName);
        if (planePrefab != null)
        {
            Instantiate(planePrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogError("Plane not found: " + selectedPlaneName);
        }
    }

    void OnValidate()
    {
        PlayerPrefs.SetString("SelectedPlane", selectedPlaneName);
    }


}
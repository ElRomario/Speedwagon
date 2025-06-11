using UnityEngine;

public class ModelSpawner : MonoBehaviour
{
    [Header("Контейнер для модели самолёта")]
    public Transform modelContainer;

    private GameObject currentModel;


    private void Start()
    {
        SpawnModel(GameManager.Instance.SelectedPlaneName);
        Debug.Log("Спавним самолёт: " + GameManager.Instance.SelectedPlaneName);
    }
    public GameObject SpawnModel(string modelName)
    {
        if (modelContainer == null)
        {
            Debug.LogError("ModelSpawner: modelContainer не назначен!");
            return null;
        }

        
        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        
        GameObject prefab = Resources.Load<GameObject>("Planes/" + modelName);
        if (prefab == null)
        {
            Debug.LogError("ModelSpawner: Не найден префаб самолёта: " + modelName);
            return null;
        }

        
        currentModel = Instantiate(prefab, modelContainer);
        currentModel.transform.localPosition = Vector3.zero;
        currentModel.transform.localRotation = Quaternion.Euler(0, 180f, 0);

        return currentModel;
    }
}

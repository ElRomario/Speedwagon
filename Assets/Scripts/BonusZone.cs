using UnityEngine;

public class BonusZone : MonoBehaviour
{
    [SerializeField] private string bonus;
   


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (bonus)
            {
                case "missiles":
                    GameManager.Instance.setMissileCollected(true);
                    break;
                case "armour":
                    GameManager.Instance.setArmourCollected(true);
                    break;
                case "plane":
                    GameManager.Instance.setPlaneCollected(true);
                    break;
            }
        }
    }
}

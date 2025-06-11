using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelWallDamage : MonoBehaviour
{


    public int damageAmount = 20; // ������� ����� ��������

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GetComponent<MeshCollider>().enabled)
        {
            Destroy(other.gameObject);
           
            
        }
    }
}

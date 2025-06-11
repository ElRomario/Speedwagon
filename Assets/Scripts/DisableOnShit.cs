using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnShit : MonoBehaviour
{
   
      public float disableTime = 5f; // ����� �� ����������
      public GameObject objectToEnable; // ������, ������� ����� �������� ����� ���������� ��������

        void OnEnable()
        {
            Invoke("DisableAndActivate", disableTime);
        }

        void DisableAndActivate()
        {
            gameObject.SetActive(false); // ��������� ������� ������

            if (objectToEnable != null)
            {
                objectToEnable.SetActive(true); // �������� ������ ������
            }
        }
    

}

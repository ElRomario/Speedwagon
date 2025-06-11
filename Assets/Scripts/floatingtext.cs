using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class FloatingText : MonoBehaviour
    {
        public float floatDistance = 10f; // ��������� ������ ����� ����������
        public float floatSpeed = 1f; // �������� ��������

        private RectTransform rectTransform;
        private Vector2 startPosition;

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            startPosition = rectTransform.anchoredPosition;
        }

        void Update()
        {
            float offset = Mathf.Sin(Time.time * floatSpeed) * floatDistance;
            rectTransform.anchoredPosition = startPosition + new Vector2(0, offset);
        }
    }


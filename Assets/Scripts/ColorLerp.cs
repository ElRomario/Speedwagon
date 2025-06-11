using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorLerpText : MonoBehaviour
{
    public Text targetText; // ������ �� ��������� Text
    public Color targetColor = Color.red; // ����, � �������� ����� �������� �����
    public float speed = 1f; // �������� ��������� �����

    private Color startColor; // �������� ���� ������
    private bool isFadingToTarget = true; // ����������� ����� �����

    void Start()
    {
        if (targetText == null)
        {
            targetText = GetComponent<Text>(); // ����-����������� ���������� Text
        }
        startColor = targetText.color;
    }

    void Update()
    {
        if (isFadingToTarget)
        {
            targetText.color = Color.Lerp(targetText.color, targetColor, speed * Time.deltaTime);
            if (Mathf.Approximately(targetText.color.r, targetColor.r) &&
                Mathf.Approximately(targetText.color.g, targetColor.g) &&
                Mathf.Approximately(targetText.color.b, targetColor.b))
            {
                isFadingToTarget = false;
            }
        }
        else
        {
            targetText.color = Color.Lerp(targetText.color, startColor, speed * Time.deltaTime);
            if (Mathf.Approximately(targetText.color.r, startColor.r) &&
                Mathf.Approximately(targetText.color.g, startColor.g) &&
                Mathf.Approximately(targetText.color.b, startColor.b))
            {
                isFadingToTarget = true;
            }
        }
    }
}

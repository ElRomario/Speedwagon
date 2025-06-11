using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{

    [Header("����� ��� ��������")]
    public Color color1 = Color.white;  // ������ ����
    public Color color2 = Color.red;    // ������ ����
    public Color color3 = Color.blue;   // ��������� ���� ����� �������������

    [Header("��������� ��������")]
    public float duration = 2f;         // ����� ����� �����
    public float pauseBetweenColors = 1f; // ���������� ����� ������� ������
    public float fadeDuration = 2f;     // ����� ������������ �������

    private Renderer objRenderer;       // �������� �������
    private Material objMaterial;       // �������� �������

    void Start()
    {
        objRenderer = GetComponent<Renderer>();

        if (objRenderer != null)
        {
            objMaterial = objRenderer.material;
            StartCoroutine(AnimateColors());
        }
    }

    IEnumerator AnimateColors()
    {
        yield return ChangeColor(color1, color2, duration);
        yield return new WaitForSeconds(pauseBetweenColors);

        yield return ChangeColor(color2, color3, duration);
        yield return new WaitForSeconds(pauseBetweenColors);

        yield return FadeOut(fadeDuration); // ��������� ������������
        Destroy(transform.parent.gameObject); // ������� ������������ ������
    }

    IEnumerator ChangeColor(Color fromColor, Color toColor, float time)
    {
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            objMaterial.color = Color.Lerp(fromColor, toColor, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objMaterial.color = toColor;
    }

    IEnumerator FadeOut(float time)
    {
        float elapsedTime = 0;
        Color startColor = objMaterial.color;

        while (elapsedTime < time)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / time);
            objMaterial.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objMaterial.color = new Color(startColor.r, startColor.g, startColor.b, 0f); // ����������� ������ ������������
    }
}

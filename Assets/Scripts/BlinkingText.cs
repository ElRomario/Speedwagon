using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlinkingText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public CardMover cardMoverShip;
    public CardMover cardMoverMissiles;
    public CardMover cardMoverArmour;

    public float blinkInterval = 0.5f;
    private float duration = 5f;
    

    void Start()
    {
        if (text == null)
            text = GetComponent<TextMeshProUGUI>();
       
        StartCoroutine(BlinkCoroutine());
    }

    IEnumerator BlinkCoroutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            text.enabled = !text.enabled; 
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }
        text.enabled = !text.enabled;
        cardMoverShip.MoveToPosition();
        yield return new WaitForSeconds(0.5f);
        cardMoverMissiles.MoveToPosition();
         yield return new WaitForSeconds(0.5f);
        cardMoverArmour.MoveToPosition();

    }
}

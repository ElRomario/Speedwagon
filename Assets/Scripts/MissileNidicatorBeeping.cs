using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WarningTextController : MonoBehaviour
    {
    public TMP_Text warningText;
    public Image warningImage;
    public float blinkInterval = 0.5f; 

        private bool isBlinking = false;

        
        public void StartBlinking()
        {
            if (!isBlinking)
            {
                isBlinking = true;
                StartCoroutine(BlinkText());
            }
        }

        
        public void StopBlinking()
        {
            isBlinking = false;
            warningText.enabled = false;
            warningImage.enabled = false;
        }

        private IEnumerator BlinkText()
        {
            while (isBlinking)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.blip);
                warningImage.enabled = !warningImage.enabled;

                warningText.enabled = !warningText.enabled; 
                yield return new WaitForSeconds(blinkInterval); 
            }
            warningText.enabled = true;
            warningImage.enabled = true;
        }
    }


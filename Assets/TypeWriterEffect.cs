using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffectLegacy : MonoBehaviour
{

    public Text textComponent;
    public string[] lines;
    public GameObject[] schemas;
    public float letterDelay = 0.05f;
    public AudioClip typeSound;
    public AudioSource audioSource;
    public GameObject image;
    public ImageScalerTitle transitionImage;
    public GameObject previousInput;

    public Animator animator;
    public string talkingState = "Talking";
    public string idleState = "Idle";

    private int currentImageIndex = 0;
    private int currentLineIndex = 0;
    private bool isTyping = false;

    private void Start()
    {
        previousInput.SetActive(false);
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        textComponent.text = "";

        while (currentLineIndex < lines.Length)
        {
            string line = lines[currentLineIndex];
            isTyping = true;

            
            if (animator != null)
                animator.Play(talkingState);

            textComponent.text = "";

            for (int i = 0; i < line.Length; i++)
            {
                textComponent.text += line[i];

                if (!char.IsWhiteSpace(line[i]) && typeSound != null && audioSource != null)
                    audioSource.PlayOneShot(typeSound);

                yield return new WaitForSeconds(letterDelay);
            }

            if (currentImageIndex < schemas.Length)
            {
                schemas[currentImageIndex].SetActive(true);
                Debug.Log("CurrentImageIndex =" + currentImageIndex);
            }
            if(currentImageIndex > 0)
            {
                schemas[currentImageIndex - 1].SetActive(false);
                Debug.Log("CurrentImageIndex Of Disable = "+ currentImageIndex);
            }
            
            if (animator != null)
                animator.Play(idleState);

            isTyping = false;
            
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0));
            
            currentImageIndex++;
            currentLineIndex++;
        }

        textComponent.text = "";
        image.SetActive(true);
        transitionImage.StartScaling();
        
    }


}

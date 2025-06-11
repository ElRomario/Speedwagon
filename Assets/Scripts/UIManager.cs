using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } 
    public Health health;
    public Canvas uiCanvas; 
    public Image deathImage;
    public Canvas uiDeath;
    public SceneReloader sceneReloader;

    public GameObject deathText;
    public GameObject deathText2;
    public GameObject Card1;
    public GameObject Card2;
    public GameObject Card3;


    private void Start()
    {
        health.OnDeath += HandleDeath;
    }
    private void Awake()
    {
        
        
        if (Instance == null)
        {
            Instance = this; 
        }
        else
        {
            Destroy(gameObject); 
            return;
        }

        
    }

    public void HandleDeath()
    {

       
        uiCanvas.enabled = false;
        uiDeath.GetComponent<Canvas>().enabled = true;
        deathImage.GetComponent<ImageScaler>().enabled = true;


        deathText.SetActive(true);
        deathText2.SetActive(true);
        sceneReloader.enabled = true;
        
    }
}

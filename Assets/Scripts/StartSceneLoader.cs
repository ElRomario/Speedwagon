using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneLoader : MonoBehaviour
{


    public ImageScaler imageScaler; 
    public Image image;
    public string sceneToLoad;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            
            image.gameObject.SetActive(true);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.rolll);
            
            imageScaler.enabled = true;
            imageScaler.isAnimating = true;

            
            StartCoroutine(WaitForImageScalerAndLoadScene());
        }
    }

    IEnumerator WaitForImageScalerAndLoadScene()
    {
        
        while (imageScaler.isAnimating)
        {
            yield return null; 
        }

        
        SceneManager.LoadScene(sceneToLoad);
    }
}

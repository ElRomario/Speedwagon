using UnityEngine;
using UnityEngine.UI;

public class InputTitleScreen : MonoBehaviour
{

    [SerializeField] GameObject plane;
    [SerializeField] GameObject texts;
    [SerializeField] GameObject logo1;
    [SerializeField] GameObject logo2;
    [SerializeField] GameObject image;
    [SerializeField] GameObject instruction;
    [SerializeField] GameObject Girl;
    [SerializeField] GameObject Dialog;
    [SerializeField] GameObject DialogManager;
    [SerializeField] GameObject DialogWindow;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            plane.SetActive(false);
            texts.SetActive(false);
            logo1.SetActive(false);
            logo2.SetActive(false);
            instruction.SetActive(false);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.laserShoot);
            image.SetActive(true);
            Girl.SetActive(true);
            Dialog.SetActive(true);
            DialogManager.SetActive(true);
            DialogWindow.SetActive(true);


        }
    }
}

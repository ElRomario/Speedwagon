using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveAllCards : MonoBehaviour
{
    [SerializeField] GameObject PlaneCard;
    [SerializeField] GameObject MissilesCard;
    [SerializeField] GameObject ArmourCard;

    CardMover CardMover;

    public void StartCourrutine()
    {
        StartCoroutine(MoveAllCardss());
    }

    public IEnumerator MoveAllCardss()
    {
        yield return new WaitForSeconds(1f);
        PlaneCard.GetComponent<CardMover>().MoveToPosition();
        ArmourCard.GetComponent<CardMover>().MoveToPosition();
        MissilesCard.GetComponent<CardMover>().MoveToPosition();

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

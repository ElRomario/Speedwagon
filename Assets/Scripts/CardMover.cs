using System.Collections;
using UnityEngine;

public class CardMover : MonoBehaviour
{
    public float moveDuration = 0.5f; 
    public AnimationCurve moveCurve;
    public float heigthOfMove;
    public MoveAllCards moveAllCards;

    private Coroutine moveCoroutine;

    
    public void MoveToPosition()
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MoveCardRoutine());
    }

    private IEnumerator MoveCardRoutine()
    {
       
        Vector3 startPosition = transform.position;

        Vector3 targetPosition = startPosition + new Vector3(0, heigthOfMove);
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);
            float curveT = moveCurve != null ? moveCurve.Evaluate(t) : t;

            transform.position = Vector3.Lerp(startPosition, targetPosition, curveT);
            
            yield return null;
        }

        transform.position = targetPosition;
        
    }

    
}

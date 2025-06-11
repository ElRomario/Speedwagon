using UnityEngine;

public class InfiniteScroll : MonoBehaviour
{
    public RectTransform[] texts; 
    public float scrollSpeed = 30f; 
    public float resetThreshold = 200f; 
    public float spacing = 30f; 

    void Update()
    {
        foreach (RectTransform t in texts)
        {
            t.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

            
            if (t.anchoredPosition.y > resetThreshold)
            {
                
                float minY = t.anchoredPosition.y;
                foreach (var other in texts)
                {
                    if (other == t) continue;
                    if (other.anchoredPosition.y < minY)
                        minY = other.anchoredPosition.y;
                }

                
                t.anchoredPosition = new Vector2(t.anchoredPosition.x, minY - spacing);
            }
        }
    }
}


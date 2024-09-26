using UnityEngine;

public class InfiniteScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 5f;
    private float backgroundHeight;
    private Vector2 startPos; 

    void Start()
    {
        startPos = transform.position;
        backgroundHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void LateUpdate()
    {
        transform.Translate(Vector2.down * scrollSpeed * Time.deltaTime);
        if (transform.position.y <= startPos.y - backgroundHeight)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + backgroundHeight);
        }
    }
}

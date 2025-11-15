using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 0.5f;
    private float bottomBoundary;

    void Start()
    {
        // Dynamically calculate bottom boundary from Ground GameObject
        GameObject ground = GameObject.FindGameObjectWithTag("Ground");
        if (ground != null)
        {
            // Try SpriteRenderer bounds first (common for ground sprites)
            SpriteRenderer spriteRenderer = ground.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                bottomBoundary = spriteRenderer.bounds.min.y;
            }
            else
            {
                // Try Collider2D bounds (if it has a collider)
                Collider2D collider = ground.GetComponent<Collider2D>();
                if (collider != null)
                {
                    bottomBoundary = collider.bounds.min.y;
                }
                else
                {
                    // Last resort: use transform position
                    bottomBoundary = ground.transform.position.y - 1f;
                }
            }
        }
        else
        {
            // Fallback if Ground tag not found
            bottomBoundary = -6f;
        }
    }

    void Update()
    {
        // Move straight down every frame
        transform.position += Vector3.down * speed * Time.deltaTime;

        // Destroy enemy if it leaves the map (goes below bottom boundary)
        if (transform.position.y < bottomBoundary)
        {
            Destroy(gameObject);
        }
    }
}

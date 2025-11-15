using UnityEngine;

public class BaseController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Destroy the enemy that reached the base
            Destroy(other.gameObject);

            // Notify GameManager to damage the base
            if (GameManager.Instance != null)
            {
                GameManager.Instance.DamageBase();
            }
        }
    }
}

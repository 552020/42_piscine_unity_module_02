using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        // Move straight down every frame
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;

    float timer = 0f;

    void Update()
    {
        // Don't spawn if game is over
        if (GameManager.Instance != null && GameManager.Instance.gameOver)
            return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
    }
}

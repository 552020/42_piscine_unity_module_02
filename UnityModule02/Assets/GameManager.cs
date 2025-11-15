using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int baseHP = 5;
    public bool gameOver = false;

    void Awake()
    {
        Instance = this;   // Singleton pattern
    }

    public void DamageBase()
    {
        if (gameOver) return;

        baseHP--;
        Debug.Log("Base HP: " + baseHP);

        if (baseHP <= 0)
            GameOver();
    }

    void GameOver()
    {
        gameOver = true;

        Debug.Log("Game Over");

        // Stop all spawners
        EnemySpawner[] spawners = FindObjectsOfType<EnemySpawner>();
        foreach (var s in spawners)
            s.enabled = false;

        // Destroy all enemies
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        foreach (var e in enemies)
            Destroy(e.gameObject);
    }
}

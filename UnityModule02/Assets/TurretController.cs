using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("Turret Stats")]
    public float fireInterval = 1f;   // time between shots
    public float baseDamage = 0.2f;   // damage per bullet

    [Header("References")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    private float fireTimer = 0f;
    private readonly List<EnemyController> enemiesInRange = new List<EnemyController>();

    void Update()
    {
        // If you have a GameManager with gameOver:
        if (GameManager.Instance != null && GameManager.Instance.gameOver)
            return;

        // Clean up null entries (destroyed enemies)
        enemiesInRange.RemoveAll(e => e == null);

        if (enemiesInRange.Count == 0)
            return;

        fireTimer += Time.deltaTime;

        if (fireTimer >= fireInterval)
        {
            fireTimer = 0f;
            EnemyController target = GetClosestEnemy();
            if (target != null)
            {
                Shoot(target);
            }
        }
    }

    // EnemyController GetClosestEnemy()
    // {
    //     EnemyController closest = null;
    //     float bestDistSq = float.MaxValue;

    //     foreach (var e in enemiesInRange)
    //     {
    //         if (e == null) continue;

    //         float dSq = (e.transform.position - transform.position).sqrMagnitude;
    //         if (dSq < bestDistSq)
    //         {
    //             bestDistSq = dSq;
    //             closest = e;
    //         }
    //     }

    //     return closest;
    // }

	EnemyController GetClosestEnemy()
{
    EnemyController best = null;
    float bestDist = Mathf.Infinity;

    foreach (var e in enemiesInRange)
    {
        float d = Vector2.Distance(e.transform.position, transform.position);

        if (d < bestDist)
        {
            bestDist = d;
            best = e;
        }
    }

    return best;
}


    void Shoot(EnemyController target)
    {
        if (bulletPrefab == null || firePoint == null || target == null)
            return;

        Vector3 dir = (target.transform.position - firePoint.position).normalized;

        GameObject b = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet = b.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Init(dir, baseDamage);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var e = other.GetComponent<EnemyController>();
            if (e != null && !enemiesInRange.Contains(e))
                enemiesInRange.Add(e);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var e = other.GetComponent<EnemyController>();
            if (e != null)
                enemiesInRange.Remove(e);
        }
    }
}

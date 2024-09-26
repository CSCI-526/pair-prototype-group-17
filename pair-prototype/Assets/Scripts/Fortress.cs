using UnityEngine;
using System.Collections;

public class Fortress : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; // Bullet prefab
    [SerializeField] private Transform firePoint;     // The position where bullets are fired
    [SerializeField] private float fireRate = 1f;     // The rate at which bullets are fired
    [SerializeField] private float range = 5f;        // Range to detect enemies

    private float fireCountdown = 0f;                 // Cooldown time for shooting
    private Transform target;                         // Current target to attack

    void Update()
    {
        // If there is a target and the cooldown time is over, fire a bullet
        if (target != null)
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
        else
        {
            // If there is no target, find enemies within range
            FindTarget();
        }
    }

    // Find the closest enemy to target
    void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);
        float shortestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = hit.transform;
                }
            }
        }

        // Set the nearest enemy as the target
        if (nearestEnemy != null)
        {
            target = nearestEnemy;
        }
        else
        {
            target = null;
        }
    }

    // Fire a bullet
    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    // Visualize the attack range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

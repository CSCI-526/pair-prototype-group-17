using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Unit
{
    [SerializeField] protected GameObject bulletPrefab; 
    [SerializeField] protected Transform firePoint;     // The position where bullets are fired
    public float fireRate = 0.3f;     // The rate at which bullets are fired
    public float range = 5f;        // Range to detect enemies
    public float fireCountdown = 0.2f;                 // Cooldown time for shooting
    protected Transform target = null;

    void Update()
    {
        ShootIfReady();
    }

    void FindTarget() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);
        Debug.Log(hits.Length);
        float shortestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (Collider2D hit in hits) {
            Unit targetUnit = hit.GetComponent<Unit>();
            if (targetUnit != null && targetUnit.isFriendly != this.isFriendly) {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);
                if (distanceToEnemy < shortestDistance) {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = hit.transform;
                }
            }
        }

        // Set the nearest enemy as the target
        if (nearestEnemy != null) {
            target = nearestEnemy;
        } else {
            target = null;
        }
    }

    // Fire a bullet
    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null) {
            bullet.Seek(target);
        }
    }

    // Visualize the attack range
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    // returns true if in ready/shooting state; 
    // false if still finding target
    public bool ShootIfReady() {
        if (target != null) {
            if (fireCountdown <= 0f) {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
            return true;
        } else {
            FindTarget();  // If there is no target, find enemies within range
            return false;
        }
    }
}


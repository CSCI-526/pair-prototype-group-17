using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab; 
    [SerializeField] protected Transform firePoint;     
    public float fireRate = 0.3f;     
    public float range = 5f;
    public float fireCountdown = 0.2f;
    protected Transform target = null;

    public void Update()
    {
        ShootIfReady();
    }

    public void FindTarget() 
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);
        float shortestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (Collider2D hit in hits) 
        {
            Unit targetUnit = hit.GetComponent<Unit>();
            if (targetUnit != null && !targetUnit.isFriendly) 
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);
                if (distanceToEnemy < shortestDistance) 
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = hit.transform;
                }
            }
        }

        target = nearestEnemy != null ? nearestEnemy : null;
    }

    public void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null) 
        {
            bullet.Seek(target);
        }
    }

    public bool ShootIfReady() 
    {
        if (target != null) 
        {
            TurnTowardsTarget();
            if (fireCountdown <= 0f) 
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
            return true;
        }
        else 
        {
            FindTarget();
            return false;
        }
    }

    private void TurnTowardsTarget()
    {
        Vector2 direction = target.position - transform.position;
        // for some reasons it needs a 90 degree offset
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;  
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

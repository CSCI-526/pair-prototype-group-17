using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trooper : Unit
{
    public float moveSpeed = 3f;
    [SerializeField] protected GameObject bulletPrefab; 
    [SerializeField] protected Transform firePoint;     
    public float fireRate = 0.3f;     
    public float range = 5f;        
    public float fireCountdown = 0.2f;                 
    protected Transform target = null;
    public abstract void Move();
    public void Update()
    {
        if (!ShootIfReady()) Move();  // if shooting, stand still; otherwise move
    }

    public void updateRange(int value){
        range += value;
    }
    public void updateSpeed(int value){
        moveSpeed += value;
    }

    public void FindTarget() 
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);
        float shortestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (Collider2D hit in hits) 
        {
            Unit targetUnit = hit.GetComponent<Unit>();
            if (targetUnit != null && targetUnit.isFriendly != this.isFriendly) 
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

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

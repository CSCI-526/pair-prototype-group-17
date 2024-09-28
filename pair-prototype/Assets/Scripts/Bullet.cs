using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 20;  // Damage value of the bullet

    private Transform target;

    // Allows the bullet to lock onto a target
    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);  // If the target disappears, destroy the bullet
            return;
        }

        // Bullet moves towards the target
        Vector2 direction = (target.position - transform.position).normalized;
        float distanceThisFrame = speed * Time.deltaTime;

        transform.Translate(direction * distanceThisFrame, Space.World);

        // Destroy the bullet when it gets close to the target
        if (Vector2.Distance(transform.position, target.position) <= 0.2f)
        {
            HitTarget();
        }
    }

    void HitTarget()
    {
        // Call the target's TakeDamage method to deal damage
        Unit unit = target.GetComponent<Unit>();
        if (unit != null)
        {
            unit.TakeDamage(damage);  // Deal damage to the enemy
        }

        Destroy(gameObject);  // Destroy the bullet upon hitting the target
    }
}

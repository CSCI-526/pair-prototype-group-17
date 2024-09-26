using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trooper : MonoBehaviour
{
    public float moveSpeed = 10f;
    public int maxHp = 100;  // maximum health point
    private int hp;  // current health point

    public abstract void Move();

    void Start()
    {
        hp = maxHp;
    }
    
    void Update()
    {
        Move();
    }

    public void TakeDamage(int damage) {
        hp -= damage;
        if (hp <= 0) Die();  // Die if healthpoint is <= 0
    }

    public void Die() {
        Destroy(gameObject);
    }
}

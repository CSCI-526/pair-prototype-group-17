using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int maxHp = 100;  // maximum health point
    protected int hp;  // current health point
    public bool isFriendly = true;

    public virtual void Start() {
        hp = maxHp;
    }
    public void TakeDamage(int damage) {
        hp -= damage;
        if (hp <= 0) Die();  // Die if healthpoint is <= 0
    }

    public virtual void Die() {
        Destroy(gameObject);
    }
}

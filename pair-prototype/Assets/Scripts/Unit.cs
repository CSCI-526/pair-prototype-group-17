using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int maxHp = 100;  // maximum health point
    public int hp;  // current health point
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

    public void boostHealth(int HpValue) {
        hp += HpValue;
    }

    public void updateMaxHp(int value){
        maxHp += value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrooper : Trooper
{
    public override void Move() {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
        
        if (transform.position.y < -Camera.main.orthographicSize) Die(); 
    }
}

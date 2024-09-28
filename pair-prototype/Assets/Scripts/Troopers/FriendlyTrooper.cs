using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyTrooper : Trooper
{
    public override void Move()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        
        if (transform.position.y > Camera.main.orthographicSize) Die(); 
    }
}

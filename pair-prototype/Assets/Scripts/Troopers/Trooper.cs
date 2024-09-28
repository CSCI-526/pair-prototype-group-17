using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trooper : Shooter
{
    public float moveSpeed = 3f;

    public abstract void Move();
    
    void Update()
    {
        if (!ShootIfReady()) Move();  // if shooting, stand still; otherwise move
    }
}

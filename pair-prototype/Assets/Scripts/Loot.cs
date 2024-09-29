using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public int gearAmount = 10;
    public void Update() 
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }
}

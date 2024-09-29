using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrooper : Trooper
{
    public GameObject lootType;
    public float lootDropRate = 0.9f;
    public float lootValueMean = 10f;
    public float lootValueVariance = 3f;


    public override void Move() {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
        if (transform.position.y < -Camera.main.orthographicSize) Die(); 
    }


    // returns 1 calculated gaussian value according to mean and variance
    float GaussianRandom(float mean, float variance)
    {
        // generate uniform distributed u1 and u2
        // u1 ~ U(0,1), u2 ~ U(0,1)
        float u1 = Random.value;
        float u2 = Random.value;

        // apply Box–Muller transform to transform 2 uniformly distributed
        // values to 1 gaussian distributed value
        // https://en.wikipedia.org/wiki/Box–Muller_transform
        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2); // Standard normal distribution
        return mean + Mathf.Sqrt(variance) * randStdNormal;  // Scale and shift to mean and variance
    }

    public override void Die()
    {
        float rand_float = Random.value;
        if (rand_float <= lootDropRate)
        {
            GameObject lootDropped = Instantiate(lootType, transform.position, Quaternion.identity);            
            Loot loot = lootDropped.GetComponent<Loot>();
            if (loot != null)
            {
                loot.gearAmount = (int)Mathf.Ceil(GaussianRandom(lootValueMean, lootValueVariance));
            }
        }
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBase : Unit
{
    private PlayerKeypressSpawner spawner;

    public override void Start()
    {
        base.Start();
        spawner = GetComponentInChildren<PlayerKeypressSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Loot loot = other.GetComponent<Loot>();
        if (loot != null)
        {
            if (spawner != null) spawner.AddGear(loot.gearAmount);
            Destroy(other.gameObject);
        }
    }
}

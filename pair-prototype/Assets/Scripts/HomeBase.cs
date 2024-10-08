using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBase : Unit
{
    private PlayerKeypressSpawner spawner;
    private Gears gears;

    public override void Start()
    {
        base.Start();
        spawner = GetComponentInChildren<PlayerKeypressSpawner>();
        gears = GameObject.Find("GearCountText").GetComponent<Gears>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Loot loot = other.GetComponent<Loot>();
        if (loot != null)
        {
            if (spawner != null){
                // spawner.AddGear(loot.gearAmount);
                gears.UpdateGearCount(loot.gearAmount);
            } 
            Destroy(other.gameObject);
        }
    }
}

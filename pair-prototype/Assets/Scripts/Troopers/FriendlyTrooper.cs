using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendlyTrooper : Trooper
{
    private int healthBoostValue = 200;
    private int gearCostPerUpgrade = 50;
    public override void Move()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        
        if (transform.position.y > Camera.main.orthographicSize) Die(); 
    }

    public void OnMouseDown()
    {
        // Upgrade defenders (by changing their color for now))
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        boostHealth(healthBoostValue);
        sprite.color = Color.yellow;


        Gears gears = GameObject.Find("GearCountUI").GetComponent<Gears>();
        if(gears.gearCount >= gearCostPerUpgrade)
        {
            gears.UpdateGearCount(-gearCostPerUpgrade);
            gears.UpdateGearUI();
        }
        print(hp);
    }
}

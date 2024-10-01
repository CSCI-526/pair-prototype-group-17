using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gears : MonoBehaviour
{
    private Text gearText;
    public int gearCount = 100;
    public int gearCostPerSpawn = 10;

    void Start(){
        gearText = GameObject.Find("GearCountUI").GetComponent<Text>();
    }

    public int getGearCostPerSpawn(){
        return gearCostPerSpawn;
    }
    public int getGearCount(){
        return gearCount;
    }

    public void UpdateGearCount(int amount) 
    {
        gearCount += amount;
        UpdateGearUI();
    }

    public void UpdateGearUI()
    {
        if (gearText != null) {
            gearText.text = "Gears: " + gearCount.ToString();
        }
    }
}

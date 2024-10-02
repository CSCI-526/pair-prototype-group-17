using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] private GameObject UpgradeCanvas;
    
    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;  // Freeze the game
        isPaused = true;
        UpgradeCanvas.SetActive(true); 
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;  // Resume the game
        isPaused = false;
        UpgradeCanvas.SetActive(false); 
    }
}

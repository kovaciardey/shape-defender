using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float totalTime = 60f; // Total time for the countdown
    public Text timerText; // Reference to the UI text element to display the timer
    
    private float timeRemaining; // Time remaining for the countdown
    private bool timerRunning; // Flag to check if the timer is running
    
    void Start()
    {
        timeRemaining = totalTime; // Initialize the time remaining
        timerRunning = true; // Start the timer
    }
    
    void Update()
    {
        if (timerRunning)
        {
            // Update the time remaining
            timeRemaining -= Time.deltaTime;

            // Calculate minutes and seconds
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);

            // Update the UI text to display the time remaining in minutes and seconds
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            
            // 
            if (minutes < 0 || seconds < 0)
            {
                timerText.text = string.Format("{0:00}:{1:00}", 0, 0);
            }

            // Check if the timer has reached zero
            if (timeRemaining <= 0)
            {
                timerRunning = false; // Stop the timer
                
                PauseGame();
                
                // Perform actions when the timer hits zero (e.g., end the game)
                Debug.Log("Game Over!");
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void Unpause()
    {
        Time.timeScale = 1f;
    }
}

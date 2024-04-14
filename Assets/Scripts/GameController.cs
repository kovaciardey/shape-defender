using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float totalTime = 60f; // Total time for the countdown
    public Text timerText; // Reference to the UI text element to display the timer

    public GameObject instructionsMenu;
    
    private float _timeRemaining; // Time remaining for the countdown
    private bool _timerRunning; // Flag to check if the timer is running
    
    private bool _isPaused = false;
    
    void Start()
    {
        _timeRemaining = totalTime; // Initialize the time remaining
        _timerRunning = true; // Start the timer
        
        // start the game as paused
        PauseGame(); 
    }
    
    void Update()
    {
        if (_timerRunning)
        {
            // Update the time remaining
            _timeRemaining -= Time.deltaTime;

            // Calculate minutes and seconds
            int minutes = Mathf.FloorToInt(_timeRemaining / 60);
            int seconds = Mathf.FloorToInt(_timeRemaining % 60);

            // Update the UI text to display the time remaining in minutes and seconds
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            
            // 
            if (minutes < 0 || seconds < 0)
            {
                timerText.text = string.Format("{0:00}:{1:00}", 0, 0);
            }

            // Check if the timer has reached zero
            if (_timeRemaining <= 0)
            {
                _timerRunning = false; // Stop the timer
                
                PauseGame();
                
                // Perform actions when the timer hits zero (e.g., end the game)
                Debug.Log("Game Over!");
            }
        }
    }

    public void StartGame()
    {
        Unpause();
        
        instructionsMenu.SetActive(false);
    }

    private void PauseGame()
    {
        _isPaused = true;
        
        Time.timeScale = 0f;
    }

    private void Unpause()
    {
        _isPaused = false;
        
        Time.timeScale = 1f;
    }

    public bool IsPaused()
    {
        return _isPaused;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerWin : MonoBehaviour
{
    public float timeRemaining = 10;          // Countdown time
    public bool timerIsRunning = false;       // Tracks if the timer is active
    public Text timeText;                     // UI Text for the timer display

    public int maxTries = 2;                  // Maximum number of tries allowed
    private static int currentTries;          // Tracks remaining tries across reloads

    public GameObject winPanel;               // UI Panel for win message
    public GameObject gameOverPanel;          // UI Panel for game over message
    public Text winText;                      // Text component for win message
    public Text gameOverText;                 // Text component for game over message

    void Start()
    {
        // Initialize or reset tries at the start of the scene
        if (currentTries <= 0)
        {
            currentTries = maxTries;
        }

        if (winPanel != null) winPanel.SetActive(false); // Hide WinPanel initially
        if (gameOverPanel != null) gameOverPanel.SetActive(false); // Hide GameOverPanel initially

        timerIsRunning = true;  // Start the timer
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();

                if (timeRemaining <= 0)
                {
                    timeRemaining = 0;
                    timerIsRunning = false;
                    OnTimerEnd();
                }
            }
        }
    }

    // Updates the timer display
    void UpdateTimerDisplay()
    {
        if (timeText != null)
        {
            int seconds = Mathf.CeilToInt(timeRemaining);
            timeText.text = $"Time Remaining: {seconds:D2}";
        }
    }

    // Handles actions when the timer ends
    void OnTimerEnd()
    {
        currentTries--;
        Debug.Log("Timer ended. Remaining tries: " + currentTries);

        if (currentTries > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            ResetTimer();
        }
        else
        {
            GameOver();
        }
    }

    // Resets the timer for a new attempt
    void ResetTimer()
    {
        timeRemaining = 10;  // Reset to initial countdown
        timerIsRunning = true;
        UpdateTimerDisplay();
    }

    // Shows the Game Over panel when the player runs out of tries
    void GameOver()
    {
        Debug.Log("GameOver function called. No more tries left.");
        timerIsRunning = false;

        if (gameOverPanel != null && gameOverText != null)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = "You're Out of Tries!";
        }
    }

    // This public method can be called by GameHandler when the player wins
    public void WinLevel()
    {
        Debug.Log("WinLevel function called. Player has won.");
        timerIsRunning = false;

        if (winPanel != null && winText != null)
        {
            winPanel.SetActive(true);
            winText.text = "Congratulations! You've won!";
        }
    }

    // Function to restart the game
    public void RetryGame()
    {
        Debug.Log("RetryGame called. Resetting tries and hiding panels."); // Log for debugging
        currentTries = maxTries; // Reset tries

        // Ensure GameOver and Win panels are hidden
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
            Debug.Log("GameOverPanel set to inactive in RetryGame.");
        }

        if (winPanel != null)
        {
            winPanel.SetActive(false);
            Debug.Log("WinPanel set to inactive in RetryGame.");
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }

}

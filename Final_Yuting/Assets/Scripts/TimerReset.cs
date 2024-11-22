using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerReset : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public Text timeText;

    public int maxTries = 2;
    private static int currentTries;

    // Game Over UI
    public GameObject gameOverPanel;
    public Text gameOverText;

    private void Start()
    {
        // Reset tries if this is the first scene load or if retrying
        if (currentTries == 0 || gameOverPanel.activeSelf)
        {
            currentTries = maxTries;
        }

        // Hide Game Over panel at the start of each game
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        timerIsRunning = true;
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

                if (timeRemaining < 0)
                {
                    timeRemaining = 0;
                }
            }
            else
            {
                timerIsRunning = false;
                OnTimerEnd();
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
        if (currentTries > 0)
        {
            currentTries--;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload scene
            ResetTimer();
        }
        else
        {
            GameOver();
        }
    }

    // Resets the timer
    void ResetTimer()
    {
        timeRemaining = 10;
        timerIsRunning = true;
        UpdateTimerDisplay();
    }

    // Handles game over actions
    void GameOver()
    {
        timerIsRunning = false;

        if (gameOverPanel != null && gameOverText != null)
        {
            gameOverPanel.SetActive(true); // Show Game Over UI
            gameOverText.text = "You're Out of Tries!";
        }
    }

    // Button function to retry the game
    public void RetryGame()
    {
        currentTries = maxTries; // Reset tries
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }

    // Button function to go to main menu or Game Over scene
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Replace with your main menu or game over scene
    }
}

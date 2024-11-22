using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public Text timeText;

    private void Start()
    {
        // Automatically start the timer when the game begins
        timerIsRunning = true;
        UpdateTimerDisplay(); // Initial display of the timer
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                // Reduce the time remaining each frame
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(); // Update the displayed time

                // Clamp the time to 0 to avoid negative values
                if (timeRemaining < 0)
                {
                    timeRemaining = 0;
                }
            }
            else
            {
                // Stop the timer when it reaches zero
                timerIsRunning = false;
                OnTimerEnd(); // Optional: Call a method when the timer ends
            }
        }
    }

    // Method to update the timer display text
    void UpdateTimerDisplay()
    {
        if (timeText != null)
        {
            timeText.text = "Time Remaining: " + Mathf.Ceil(timeRemaining).ToString();
        }
    }

    // Optional method to handle events when the timer ends
    void OnTimerEnd()
    {
        Debug.Log("Timer has ended!");
    }
}

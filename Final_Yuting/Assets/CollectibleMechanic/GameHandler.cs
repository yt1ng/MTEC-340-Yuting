using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public Text CollectibleText;              // UI Text to display the number of collectibles
    public Text WinText;                      // UI Text to display the win message

    private int collectibles = 0;             // Counter for the collectibles
    public int requiredCollectibles = 7;      // Number of collectibles required to win

    private TimerWin timerWin;                // Reference to TimerWin script
    private bool hasWon = false;              // Tracks if the player has already won

    void Start()
    {
        // Initialize the win text to be empty at the start
        WinText.text = "";

        // Find the TimerWin component in the scene
        timerWin = FindObjectOfType<TimerWin>();

        // Display the initial collectible count
        UpdateCollectibleText();
    }

    // Method to increase collectible count and check for win condition
    public void SetCollectibles(int count)
    {
        if (hasWon) return; // Prevent further updates if the player has already won

        // Increase the collectible count
        collectibles += count;

        // Update the UI to show the current collectible count
        UpdateCollectibleText();

        // Check if the player has collected enough items to win
        if (collectibles >= requiredCollectibles)
        {
            WinLevel();
        }
    }

    // Updates the collectible display text
    void UpdateCollectibleText()
    {
        CollectibleText.text = "Items : " + collectibles;
    }

    // Handles level win actions
    private void WinLevel()
    {
        hasWon = true; // Set the win state to prevent duplicate calls
        WinText.text = ""; // Display win message on the screen

        // Notify the TimerWin script to stop the timer
        if (timerWin != null)
        {
            timerWin.WinLevel();
        }
    }
}

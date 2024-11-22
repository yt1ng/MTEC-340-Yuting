using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public GameHandler GH;
    public AudioClip collectibleSound;

    void Start()
    {
        if (GH == null)
        {
            GH = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player (assuming the player has the tag "Player")
        if (other.CompareTag("Player") && gameObject.CompareTag("Collectible"))
        {
            GH.SetCollectibles(1); // Increment the collectible count by 1 through SetCollectibles
            AudioSource.PlayClipAtPoint(collectibleSound, transform.position); // Play collectible sound
            Destroy(gameObject); // Destroy the collectible object
        }
    }
}

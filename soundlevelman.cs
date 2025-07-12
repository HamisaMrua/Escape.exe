using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;          // Assign the Game Over UI
    public AudioSource levelMusicAudio;    // Assign the level music AudioSource
    public AudioSource gameOverAudio;      // Assign the Game Over sound AudioSource

    public void TriggerGameOver()
    {
        // Stop the level music
        if (levelMusicAudio.isPlaying)
            levelMusicAudio.Stop();

        // Show Game Over UI
        gameOverUI.SetActive(true);

        // Play Game Over sound
        gameOverAudio.Play();
    }
}

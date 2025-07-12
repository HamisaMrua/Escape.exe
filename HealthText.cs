using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI gameOverText;
    public GameObject gameOverPanel;
    public Image gameOverSkull;

    [Header("Audio")]
    public AudioSource levelMusicAudio;
    public AudioSource gameOverAudio;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        if (gameOverText != null) gameOverText.gameObject.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (gameOverSkull != null) gameOverSkull.gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = "Health: " + currentHealth;
    }

    void GameOver()
    {
        Time.timeScale = 0f;

        // ⛔ Stop level music
        if (levelMusicAudio != null && levelMusicAudio.isPlaying)
            levelMusicAudio.Stop();

        // 🔊 Play Game Over sound
        if (gameOverAudio != null)
            gameOverAudio.Play();

        if (gameOverText != null) gameOverText.gameObject.SetActive(true);
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (gameOverSkull != null) gameOverSkull.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

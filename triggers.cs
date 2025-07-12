using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel13Trigger : MonoBehaviour
{
    [Header("Level Settings")]
    public string levelToLoad = "level 13";

    private bool hasLoaded = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasLoaded) return;

        if (collision.collider.CompareTag("Player"))
        {
            hasLoaded = true;
            Debug.Log("🎮 Player landed on platform. Loading Level 13...");
            SceneManager.LoadScene(levelToLoad);
        }
    }
}

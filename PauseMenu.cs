using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    void Start()
    {
        pauseMenu.SetActive(false);     // Ensure it's hidden at the start
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu.activeSelf)
            {
                // Pause game
                Time.timeScale = 0f;
                pauseMenu.SetActive(true); // ✅ FIXED: Show menu
                Cursor.visible = true;
            }
            else
            {
                // Resume game
                resume();
            }
        }
    }

    public void resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
    }

    public void quit()
    {
        Application.Quit();
    }
}

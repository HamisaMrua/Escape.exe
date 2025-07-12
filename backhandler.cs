using UnityEngine;

public class BackToMenu : MonoBehaviour
{
    public GameObject mainMenu;    // Drag your MainMenu GameObject here
    public GameObject currentPanel; // Drag the current panel (e.g., Settings, GameUI, etc.)

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        currentPanel.SetActive(false);
    }
}

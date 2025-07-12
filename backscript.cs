using UnityEngine;

public class backscript : MonoBehaviour
{
    public GameObject MainMenu;

    public void ShowMainMenu()
    {
        if (MainMenu != null)
        {
            MainMenu.SetActive(true);
        }
    }
}

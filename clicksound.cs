using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    public GameObject optionMenu;
    public AudioSource audioSource;
    public AudioClip clickSound;

    public void ShowOptionMenu()
    {
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        if (optionMenu != null)
        {
            optionMenu.SetActive(true);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevelButton : MonoBehaviour
{
    // Optional: drag your Button from the Inspector to this field
    public Button Button;

    void Start()
    {
        if (Button != null)
        {
            Button.onClick.AddListener(LoadLevel11);
        }
    }

    // This function will be called when the button is clicked
    public void LoadLevel11()
    {
        // Option 1: by Scene Name
        SceneManager.LoadScene("Tutorial");

        // Option 2: by Build Index (uncomment if needed)
        // SceneManager.LoadScene(11);
    }
}

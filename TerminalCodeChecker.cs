using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TerminalCodeChecker : MonoBehaviour
{
   
    [Header("UI References")]
    public InputField codeInputField;  // Drag CodeInputField here
    public Text feedbackText;          // Drag Feedback here

    [Header("Answer Settings")]
    public string correctCode = "a";   // Set your correct code
    public int requiredFragments = 3;     // Required number of fragments

    private int fragmentsCollected = 0;

    // Call this when a fragment is collected in your game logic
    public void CollectFragment()
    {
        fragmentsCollected++;
    }

    // Connect this method to SubmitButton's OnClick()
    public void OnSubmitButtonClicked()
    {
        string enteredCode = codeInputField.text;

        if (enteredCode == correctCode && fragmentsCollected >= requiredFragments)
        {
            feedbackText.text = "Correct!";
            feedbackText.color = Color.green;
            Invoke("LoadNextScene", 1f);
        }
        else
        {
            feedbackText.text = "Wrong code or not enough fragments!";
            feedbackText.color = Color.red;
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("3D");
    }
}

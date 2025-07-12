using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CodeTerminalLogic : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialogueUI;
    public Text promptText;
    public InputField codeInputField;
    public Button submitButton;
    public Text feedbackText;

    [Header("Question Logic")]
    public List<string> prompts;
    public List<string> correctAnswers;

    [Header("Fragment System")]
    public FragmentManager fragmentManager; // Drag in from Inspector

    private List<string> playerAnswers = new List<string>();
    private int currentQuestion = 0;
    private bool questionsCompleted = false;

    void Start()
    {
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(SubmitAnswer);
        }
        else
        {
            Debug.LogError("Submit Button not assigned in the Inspector!");
        }

        dialogueUI.SetActive(false);
    }

    public void ShowDialogue()
    {
        dialogueUI.SetActive(true);
        Time.timeScale = 0f;
        feedbackText.text = "";

        if (questionsCompleted)
        {
            if (fragmentManager.fragmentsCollected >= fragmentManager.totalFragments)
            {
                feedbackText.text = "✅ Fragments complete! Proceeding...";
                StartCoroutine(LoadNextLevelDelayed(1.5f));
            }
            else
            {
                feedbackText.text = "⚠️ Still missing fragments. Go collect more.";
                StartCoroutine(CloseDialogueAfterDelay(2f));
            }
        }
        else
        {
            playerAnswers.Clear();
            currentQuestion = 0;
            StartCoroutine(TypePrompt(prompts[currentQuestion]));
        }
    }

    IEnumerator TypePrompt(string fullText)
    {
        promptText.text = "";
        foreach (char c in fullText)
        {
            promptText.text = fullText;
            yield return new WaitForSeconds(0.02f);
        }

        codeInputField.text = "";
        feedbackText.text = "";
        codeInputField.ActivateInputField();
    }

    void SubmitAnswer()
    {
        if (currentQuestion >= correctAnswers.Count)
        {
            Debug.LogWarning("Current question index out of range!");
            return;
        }

        string input = codeInputField.text.Trim();
        playerAnswers.Add(input);

        if (input == correctAnswers[currentQuestion])
        {
            feedbackText.text = "✅ Correct!";
            currentQuestion++;

            if (currentQuestion < prompts.Count)
            {
                StartCoroutine(NextPrompt());
            }
            else
            {
                questionsCompleted = true;

                if (fragmentManager.fragmentsCollected >= fragmentManager.totalFragments)
                {
                    feedbackText.text = "✅ All correct & fragments complete!";
                    StartCoroutine(LoadNextLevelDelayed(1f));
                }
                else
                {
                    feedbackText.text = "✅ Answers done, but fragments missing!";
                    StartCoroutine(CloseDialogueAfterDelay(2f));
                }
            }
        }
        else
        {
            feedbackText.text = "❌ Try again.";
        }
    }

    IEnumerator NextPrompt()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(TypePrompt(prompts[currentQuestion]));
    }

    IEnumerator LoadNextLevelDelayed(float delay)
    {
        Debug.Log("🎮 Loading scene '3D' in " + delay + " seconds...");
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;

        try
        {
            SceneManager.LoadScene("3D");
        }
        catch
        {
            Debug.LogError("❌ Failed to load scene '3D'. Is it added to Build Settings?");
        }
    }

    IEnumerator CloseDialogueAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        CloseDialogue();
    }

    public void CloseDialogue()
    {
        dialogueUI.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("🛑 Terminal closed. Returning to gameplay.");
    }
}

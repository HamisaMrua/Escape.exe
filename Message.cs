using System.Collections;
using UnityEngine;
using TMPro; // Required if you're using TextMeshPro
using UnityEngine.SceneManagement; // Add this to enable scene loading

public class TypewriterEffect : MonoBehaviour
{
    public TMP_Text whattodo; // Drag your Text object here
    [TextArea(3, 10)]
    public string fullMessage = "The goal of this mission is to fix a critical code leak in Layer 3.\n\n" +
                                "To succeed, you must collect all scattered code fragments...\n\n" +
                                "But beware: the T-virus is spreading through the system, corrupting everything it touches.\n\n" +
                                "Avoid infection, and navigate the unstable environment with caution.\n\n" +
                                "The fate of Layer 3 depends on you.";

    public float typingSpeed = 0.05f; // Delay between letters
    public float delayBeforeLoad = 2f; // Optional delay after text is done before changing scene

    void Start()
    {
        whattodo.text = "";
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (char letter in fullMessage)
        {
            whattodo.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Wait a moment before loading next scene (optional)
        yield return new WaitForSeconds(delayBeforeLoad);

        // Load Level 11
        SceneManager.LoadScene("Level 11");
    }
}

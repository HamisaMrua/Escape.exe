using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    // This function is called when the Play button is clicked
    public void LoadTutorialScene()
    {
        SceneManager.LoadScene("Instruction_1");
    }
}

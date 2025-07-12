using UnityEngine;
using UnityEngine.UI;

public class FragmentManager : MonoBehaviour
{
    public int totalFragments = 3;
    public int fragmentsCollected = 0;
    public Text fragmentCounterText; // Drag a UI text object here (optional)

    void Start()
    {
        UpdateUI();
    }

    public void CollectFragment()
    {
        fragmentsCollected++;
        Debug.Log("Fragment collected! Total now: " + fragmentsCollected);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (fragmentCounterText != null)
        {
            int fragmentsLeft = totalFragments - fragmentsCollected;
            fragmentCounterText.text = "Fragments Left: " + fragmentsLeft;
        }
    }
}

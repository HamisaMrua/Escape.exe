using UnityEngine;

public class OptionsSettings: MonoBehaviour
{
   
 public GameObject OptionsMenu;
    public void ShowOptionsMenu()
    {
        if (OptionsMenu != null)
        {
            OptionsMenu.SetActive(true);
        }
    }
}

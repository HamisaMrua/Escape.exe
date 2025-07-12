using UnityEngine;

public class CodeFragment : MonoBehaviour
{
    [System.Obsolete]
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<FragmentManager>().CollectFragment();
            Destroy(gameObject); // Removes the fragment
        }
    }
}

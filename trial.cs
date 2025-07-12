using UnityEngine;

public class BounceEffect : MonoBehaviour
{
    public float squashAmount = 0.9f;
    public float stretchAmount = 2f;
    public float duration = 0.1f;

    private Vector3 originalScale;
    private bool isGrounded = false;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Simple "jump" stretch trigger
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            StartCoroutine(Stretch());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isGrounded)
        {
            isGrounded = true;
            StopAllCoroutines();
            StartCoroutine(Squash());
        }
    }

    System.Collections.IEnumerator Squash()
    {
        transform.localScale = new Vector3(
            originalScale.x + squashAmount,
            originalScale.y - squashAmount,
            originalScale.z
        );
        yield return new WaitForSeconds(duration);
        transform.localScale = originalScale;
    }

    System.Collections.IEnumerator Stretch()
    {
        transform.localScale = new Vector3(
            originalScale.x - stretchAmount,
            originalScale.y + stretchAmount,
            originalScale.z
        );
        yield return new WaitForSeconds(duration);
        transform.localScale = originalScale;
    }
}


using UnityEngine;

public class BounceWithSound : MonoBehaviour
{
    private Vector3 originalScale;
    public float bounceAmount = 0.2f;
    public float duration = 0.1f;
    public AudioSource bounceSound;

    private bool hasJumped = false;

    void Start()
    {
        originalScale = transform.localScale;

        if (bounceSound == null)
            bounceSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Trigger jump manually for test (spacebar)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hasJumped = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (hasJumped && col.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(BounceEffect());

            if (!bounceSound.isPlaying)
                bounceSound.Play();

            hasJumped = false; // Reset after landing
        }
    }

    System.Collections.IEnumerator BounceEffect()
    {
        transform.localScale = new Vector3(
            originalScale.x + bounceAmount,
            originalScale.y - bounceAmount,
            originalScale.z
        );

        yield return new WaitForSeconds(duration);
        transform.localScale = originalScale;
    }
}

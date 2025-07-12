using UnityEngine;

public class BouncyPlatform : MonoBehaviour
{
    [Header("Bounce Settings")]
    public float squashAmount = 0.5f;
    public float duration = 0.005f;
    public AnimationCurve bounceCurve;

    private Vector3 originalScale;
    private Coroutine bounceRoutine;

    void Start()
    {
        originalScale = transform.localScale;

        // If no curve is assigned, use a default elastic curve
        if (bounceCurve == null || bounceCurve.length == 0)
        {
            bounceCurve = new AnimationCurve(
                new Keyframe(0, 0),
                new Keyframe(0.2f, 1.2f),
                new Keyframe(0.4f, 0.8f),
                new Keyframe(0.6f, 1.05f),
                new Keyframe(0.8f, 0.98f),
                new Keyframe(1f, 1f)
            );
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0f && collision.collider.CompareTag("Player"))
        {
            if (bounceRoutine != null)
                StopCoroutine(bounceRoutine);

            bounceRoutine = StartCoroutine(DoBounce());
        }
    }

    System.Collections.IEnumerator DoBounce()
    {
        float time = 0f;
        while (time < duration)
        {
            float progress = time / duration;
            float curveValue = bounceCurve.Evaluate(progress);

            // Apply squash/stretch animation
            transform.localScale = new Vector3(
                originalScale.x + squashAmount * (curveValue - 1),
                originalScale.y - squashAmount * (curveValue - 1),
                originalScale.z
            );

            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
    }
}

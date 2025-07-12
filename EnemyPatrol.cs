using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform pointA;
    public Transform pointB;

    private Vector3 targetPosition;

    void Start()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError("Assign PointA and PointB in the inspector.");
            enabled = false;
            return;
        }

        targetPosition = pointB.position;
    }

    void Update()
    {
        // Move towards the target point
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // When close enough to the target, switch direction
        if (Vector2.Distance(transform.position, targetPosition) < 0.05f)
        {
            targetPosition = (targetPosition == pointA.position) ? pointB.position : pointA.position;
        }
    }
}

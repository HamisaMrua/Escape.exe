using UnityEngine;

public class DynamicCameraFollow2D : MonoBehaviour
{
    [Header("Player Reference")]
    public Transform target;  // Drag your square player here

    [Header("Camera Movement")]
    public float smoothSpeed = 0.1f;

    [Header("Camera Views")]
    public Vector3[] cameraOffsets;  // Set these in Inspector or define below
    private int viewIndex = 0;

    private Vector3 currentOffset;

    void Start()
    {
        if (cameraOffsets == null || cameraOffsets.Length == 0)
        {
            // Default camera offsets: center, right side, left side, back/top
            cameraOffsets = new Vector3[]
            {
                new Vector3(0, 0, -10),     // Center follow
                new Vector3(2, 0, -10),     // Side view (right)
                new Vector3(-2, 0, -10),    // Side view (left)
                new Vector3(0, 2, -10)      // Back/top view
            };
        }

        currentOffset = cameraOffsets[0];
    }

    void Update()
    {
        // Change camera view using the "C" key
        if (Input.GetKeyDown(KeyCode.C))
        {
            viewIndex = (viewIndex + 1) % cameraOffsets.Length;
            currentOffset = cameraOffsets[viewIndex];
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + currentOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}

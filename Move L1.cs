using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public float moveSpeed = 60f;
    public float jumpForce = 50f;
    public float fallThreshold = -200f; // 👈 Customize this for your level

    private Rigidbody2D rb;
    private float moveInput;
    private bool isFacingRight = true;
    private Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position; // Save start point
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        // Flip sprite
        if (moveInput > 0 && !isFacingRight)
            Flip();
        else if (moveInput < 0 && isFacingRight)
            Flip();

        // Jump anytime (infinite jump)
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        // Check for fall
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void Respawn()
    {
        transform.position = startPosition;
        rb.linearVelocity = Vector2.zero; // Stop motion on respawn
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}

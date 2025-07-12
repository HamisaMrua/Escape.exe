using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    public float moveSpeed = 50f;
    public float jumpForce = 55f;
    public float climbSpeed = 30f;
    public float fallThreshold = -200f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isClimbing = false;
    private Vector3 startPosition;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        rb.gravityScale = 5f; // gravity when not climbing
        startPosition = transform.position;

        // Freeze rotation so the player doesn't tilt while falling
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Handle climbing
        if (isClimbing)
        {
            float verticalInput = Input.GetAxisRaw("Vertical");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, verticalInput * climbSpeed);
            rb.gravityScale = 0f;
        }
        else
        {
            rb.gravityScale = 5f;
        }

        // Fall threshold check
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Apply movement
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Flip sprite based on direction
        if (moveInput > 0)
            transform.localScale = new Vector3(4f, 4f, 1f); // face right
        else if (moveInput < 0)
            transform.localScale = new Vector3(-4f, 4f, 1f); // face left
    }

    void Respawn()
    {
        transform.position = startPosition;
        rb.linearVelocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Climbable"))
        {
            isClimbing = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }

        if (collision.gameObject.CompareTag("Climbable"))
        {
            isClimbing = false;
        }
    }
}

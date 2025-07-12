using UnityEngine;

public class CapsuleMovement2D : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public int extraJumpValue = 1;

    [Header("Wall Jump")]
    public float wallJumpForceX = 8f;
    public float wallJumpForceY = 12f;
    public float wallSlideSpeed = 0.5f;

    [Header("Gravity Control")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Check Settings")]
    public Transform groundCheck;
    public Transform wallCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;

    private float moveInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private int extraJumps;
    private bool isFacingRight = true;
    private bool jumpedFromWall;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        // Flip character sprite
        if (moveInput > 0 && !isFacingRight)
            Flip();
        else if (moveInput < 0 && isFacingRight)
            Flip();

        // Check surroundings
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, groundLayer);

        // Reset extra jumps when grounded
        if (isGrounded)
            extraJumps = extraJumpValue;
  Debug.Log("IsGrounded: " + isGrounded);
        // Wall slide
        isWallSliding = !isGrounded && isTouchingWall && moveInput != 0 && rb.linearVelocity.y < 0;
        if (isWallSliding)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlideSpeed);

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                Jump();
            }
            else if (extraJumps > 0)
            {
                Jump();
                extraJumps--;
            }
            else if (isWallSliding)
            {
                WallJump();
                jumpedFromWall = true;
                Invoke(nameof(ResetWallJump), 0.1f);
            }
        }

        // Apply gravity adjustments
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        // Animations
        //animator.SetBool("IsRunning", moveInput != 0);
        //animator.SetBool("IsGrounded", isGrounded);
        //animator.SetBool("IsWallSliding", isWallSliding);
    }

    void FixedUpdate()
    {
        if (!jumpedFromWall)
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        animator.SetTrigger("Jump");
    }

    void WallJump()
    {
        float direction = isFacingRight ? -1 : 1;
        rb.linearVelocity = new Vector2(wallJumpForceX * direction, wallJumpForceY);
        animator.SetTrigger("Jump");
    }

    void ResetWallJump()
    {
        jumpedFromWall = false;
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
        if (wallCheck != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(wallCheck.position, checkRadius);
        }
    }
}

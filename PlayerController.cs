using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public float variableJumpHeightMultiplier = 0.5f; // Adjust this value to control the jump height

    private Rigidbody2D rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Calculate the ground check position just at the bottom of the player
        Vector2 groundCheckPosition = playerGroundPosition();

        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition, groundCheckRadius, groundLayer);

        float moveX = Input.GetAxis("Horizontal");

        // Move the character horizontally
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // Jump if the player is grounded and the Jump button is pressed
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Apply variable jump height if the jump button is released early
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Calculate the ground check position just at the bottom of the player
        Vector2 groundCheckPosition = playerGroundPosition();

        // Draw a small circle to represent the ground check position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPosition, groundCheckRadius);
    }

    private Vector2 playerGroundPosition()
    {
        return (Vector2)transform.position - new Vector2(0f, GetComponent<BoxCollider2D>().bounds.extents.y);
    }
}

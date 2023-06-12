using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

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
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
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

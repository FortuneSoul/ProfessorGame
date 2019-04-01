using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InputController : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField] private float moveSpeed = 200f;
    [SerializeField] private float jumpForce = 200f;
    [SerializeField] private Transform[] groundCheckTransforms;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private int extraJumps;
    #pragma warning restore 0649

    private new Rigidbody2D rigidbody;
    private bool facingRight = true;
    private bool isGrounded;
    private float extraJumpsLeft;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        foreach (Transform feet in groundCheckTransforms)
        {
            isGrounded = Physics2D.OverlapCircle(feet.position, checkRadius, whatIsGround);
            if (isGrounded) break;
        }
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rigidbody.velocity = rigidbody.velocity.With(x: horizontalInput * Time.fixedDeltaTime * moveSpeed);
        HandleFacingDirection(horizontalInput);
    }

    private void Update()
    {
        HandleJump();
    }

    private void HandleJump()
    {
        if (isGrounded)
        {
            extraJumpsLeft = extraJumps;
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumpsLeft > 0)
        {
            rigidbody.velocity = Vector2.up * jumpForce * Time.fixedDeltaTime;
            extraJumpsLeft--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumpsLeft == 0 && isGrounded)
        {
            rigidbody.velocity = Vector2.up * jumpForce * Time.fixedDeltaTime;
        }
    }

    private void HandleFacingDirection(float horizontalInput)
    {
        if (facingRight && horizontalInput < 0)
        {
            Flip();
        }
        else if (!facingRight && horizontalInput > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}

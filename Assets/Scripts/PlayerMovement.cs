using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    AudioSource audioPlin;
    public float speed;
    public float jumpForce = 10f;
    private bool isGrounded;
    private bool jumpRequest;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioPlin = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        rb.MovePosition(rb.position + new Vector2(moveHorizontal, 0).normalized * speed * Time.fixedDeltaTime);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        animator.SetBool("IsGrounded", isGrounded);

        if (jumpRequest && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpRequest = false;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpRequest = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "coletável")
        {
            audioPlin.Play();
            GameController.Collect();
            Destroy(other.gameObject);
        }
    }
}
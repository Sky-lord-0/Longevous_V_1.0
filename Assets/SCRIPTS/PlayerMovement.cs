using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 8f;
    public float jumpForce = 14f;

    [Header("Ataque")]
    public Transform attackHitbox;
    public float attackDistance = 1f;

    [Header("Chão")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;

    [Header("Configurações do Dash")]
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;

    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 3f;
    }

    void Update()
    {
        if (isDead) return;

        if (isDashing) return;

        moveInput = 0f;

        if (Input.GetKey(KeyCode.A)) moveInput = -1f;
        if (Input.GetKey(KeyCode.D)) moveInput = 1f;

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (attackHitbox != null)
        {
            float dir = transform.localScale.x > 0 ? 1f : -1f;
            attackHitbox.localPosition = new Vector3(dir * attackDistance, 0f, 0f);
        }

        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundRadius,
            groundLayer
        );

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            PlayerAudio audioPlayer = GetComponent<PlayerAudio>();

        if (audioPlayer != null)
            audioPlayer.PlayJump();
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;

        if (isDashing) return;

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

    PlayerAudio audioPlayer = GetComponent<PlayerAudio>();

    if (audioPlayer != null)
        audioPlayer.PlayDash();

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);

        yield return new WaitForSeconds(dashingTime);

        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;
    }

    public void Die()
    {
        isDead = true;
        moveInput = 0f;
        rb.velocity = Vector2.zero;
    }
}
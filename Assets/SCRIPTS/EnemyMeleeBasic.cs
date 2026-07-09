using UnityEngine;

public class EnemyMeleeBasic : MonoBehaviour
{
    public enum State { Idle, Patrol, Chase, Attack, Stunned }

    [Header("Stats (área 1 - melee básico)")]
    public int maxHP = 120;
    public int damage = 25;
    public float attackCooldown = 1.2f;

    [Header("Movement")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.2f;
    public float chaseRange = 6f;
    public float attackRange = 1.1f;

    [Header("Patrol")]
    public Transform leftPoint;
    public Transform rightPoint;

    [Header("Stun")]
    public float stunnedTime = 0.25f;

    [Header("Visual fix")]
    public float deathVisualYOffset = -0.5f;

    private State state = State.Idle;

    private Rigidbody2D rb;
    private Transform player;
    private Animator animator;
    private SpriteRenderer sr;

    private float lastAttackTime = -999f;
    private float stunEndTime = -999f;
    private int patrolDir = 1;

    private bool isDead = false;
    private bool isAttacking = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        state = (leftPoint != null && rightPoint != null)
            ? State.Patrol
            : State.Idle;
    }

    void Update()
    {
        if (isDead || player == null) return;

        if (state == State.Stunned)
        {
            if (Time.time >= stunEndTime)
                state = State.Chase;

            return;
        }

        float dist = Vector2.Distance(transform.position, player.position);

        if (!isAttacking)
        {
            if (dist <= attackRange)
            {
                state = State.Attack;
            }
            else if (dist <= chaseRange)
            {
                state = State.Chase;
            }
            else
            {
                state = (leftPoint != null && rightPoint != null)
                    ? State.Patrol
                    : State.Idle;
            }
        }

        animator.SetBool("isRunning",
            state == State.Patrol || state == State.Chase);
    }

    void FixedUpdate()
    {
        if (isDead || player == null) return;

        switch (state)
        {
            case State.Idle:
                rb.velocity = new Vector2(0f, rb.velocity.y);
                break;

            case State.Patrol:
                PatrolMove();
                break;

            case State.Chase:
                ChaseMove();
                break;

            case State.Attack:
                Attack();
                break;

            case State.Stunned:
                break;
        }
    }

    void PatrolMove()
    {
        if (leftPoint == null || rightPoint == null)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            return;
        }

        float targetX = (patrolDir == 1)
            ? rightPoint.position.x
            : leftPoint.position.x;

        float dir = Mathf.Sign(targetX - transform.position.x);

        sr.flipX = dir < 0;

        rb.velocity = new Vector2(dir * patrolSpeed, rb.velocity.y);

        if (Mathf.Abs(transform.position.x - targetX) < 0.1f)
            patrolDir *= -1;
    }

    void ChaseMove()
    {
        float dir = Mathf.Sign(player.position.x - transform.position.x);

        sr.flipX = dir < 0;

        rb.velocity = new Vector2(dir * chaseSpeed, rb.velocity.y);
    }

    void Attack()
    {
        rb.velocity = Vector2.zero;

        if (Time.time - lastAttackTime < attackCooldown)
            return;

        lastAttackTime = Time.time;

        isAttacking = true;

        animator.SetTrigger("isAttacking");

        EnemyAudio audioEnemy = GetComponent<EnemyAudio>();

        if (audioEnemy != null)
            audioEnemy.PlayAttack();

        PlayerHealth ph = player.GetComponent<PlayerHealth>();

        if (ph != null)
            ph.TakeDamage(damage);

        Invoke(nameof(EndAttack), 0.4f);
    }

    void EndAttack()
    {
        isAttacking = false;
    }

    public void OnHitStun()
    {
        if (isDead) return;

        state = State.Stunned;
        stunEndTime = Time.time + stunnedTime;
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;

        animator.SetBool("isDead", true);

        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        transform.position += new Vector3(0f, deathVisualYOffset, 0f);
    }

    public void ApplyDeathGroundOffset()
    {
        transform.position += new Vector3(0f, deathVisualYOffset, 0f);
    }
}
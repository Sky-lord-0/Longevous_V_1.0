using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    [Header("Attack")]
    public float shootRange = 7f;
    public float shootCooldown = 1.4f;

    [Header("References")]
    public Transform shootPoint;
    public GameObject projectilePrefab;

    private Transform player;
    private Animator animator;

    private float lastShootTime;
    private bool isDead;
    private bool hasShotThisAttack;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;
        if (player == null) return;

        if (player.position.x > transform.position.x)
            transform.localScale = Vector3.one;
        else
            transform.localScale = new Vector3(-1, 1, 1);

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= shootRange)
        {
            TryShoot();
        }
    }

    void TryShoot()
    {
        if (Time.time - lastShootTime < shootCooldown)
            return;

        lastShootTime = Time.time;
        hasShotThisAttack = false;

        animator.SetTrigger("Attack");

        Invoke(nameof(ShootProjectileOnce), 0.35f);
    }

    void ShootProjectileOnce()
    {
        if (isDead) return;
        if (hasShotThisAttack) return;

        hasShotThisAttack = true;

        if (player == null || shootPoint == null || projectilePrefab == null)
            return;

        EnemyAudio audioEnemy = GetComponent<EnemyAudio>();

        if (audioEnemy != null)
            audioEnemy.PlayAttack();

        Vector2 dir = (player.position - shootPoint.position).normalized;

        GameObject proj = Instantiate(
            projectilePrefab,
            shootPoint.position,
            Quaternion.identity
        );

        EnemyProjectile ep = proj.GetComponent<EnemyProjectile>();

        if (ep != null)
            ep.Init(dir);
    }

    public void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true);
    }
}
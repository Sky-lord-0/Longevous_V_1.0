using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP = 120;
    public int CurrentHP => hp;

    [Header("Blood")]
    public int bloodOnDeath = 8;

    [Header("Knockback")]
    public float knockbackForceX = 7f;
    public float knockbackForceY = 2f;

    private int hp;
    private Rigidbody2D rb;
    private Animator animator;

    private bool isDead;

    void Awake()
    {
        hp = maxHP;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int dmg, Vector2 attackerPos)
    {
        if (isDead) return;

        hp -= dmg;

        if (animator != null)
            animator.SetTrigger("Hurt");

        EnemyAudio audioEnemy = GetComponent<EnemyAudio>();

        if (audioEnemy != null)
            audioEnemy.PlayHurt();

        ApplyKnockback(attackerPos);
        NotifyStun();

        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        EnemyAudio audioEnemy = GetComponent<EnemyAudio>();

        if (audioEnemy != null)
            audioEnemy.PlayDeath();

        GetComponent<BossMage>()?.Die();

        GetComponent<EnemyRanged>()?.Die();
        GetComponent<EnemyMeleeBasic>()?.Die();

        if (animator != null)
            animator.SetBool("isDead", true);

        HandleKillRewards();
        GiveBloodToPlayer();

        var reward = GetComponent<RewardOnDeath>();
        if (reward != null)
            reward.GiveReward();

        this.enabled = false;
        Destroy(gameObject, 3f);
    }

    void HandleKillRewards()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p == null) return;

        PlayerUpgrades upgrades = p.GetComponent<PlayerUpgrades>();
        PlayerHealth ph = p.GetComponent<PlayerHealth>();

        if (upgrades == null || ph == null) return;

        if (upgrades.lifestealOnKill)
        {
            int heal = Mathf.RoundToInt(ph.maxHP * upgrades.lifestealPercent);
            ph.currentHP = Mathf.Min(ph.currentHP + heal, ph.maxHP);
        }
    }

    void GiveBloodToPlayer()
    {
        PlayerBlood blood = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerBlood>();

        if (blood != null)
            blood.AddBlood(bloodOnDeath);
    }

    void ApplyKnockback(Vector2 attackerPos)
    {
        if (rb == null) return;

        float dir = (transform.position.x >= attackerPos.x) ? 1f : -1f;

        rb.velocity = new Vector2(0f, rb.velocity.y);

        rb.AddForce(
            new Vector2(dir * knockbackForceX, knockbackForceY),
            ForceMode2D.Impulse
        );
    }

    void NotifyStun()
    {
        if (isDead) return;

        EnemyMeleeBasic melee = GetComponent<EnemyMeleeBasic>();

        if (melee != null && melee.enabled)
            melee.OnHitStun();
    }
}
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 250;
    public int currentHP;

    [Header("UI")]
    public HealthBarUI healthBar;

    [Header("Knockback")]
    public float knockbackForceX = 8f;
    public float knockbackForceY = 3f;

    private Rigidbody2D rb;

    void Awake()
    {
        currentHP = maxHP;
        rb = GetComponent<Rigidbody2D>();

        if (healthBar != null)
            healthBar.UpdateHealth(currentHP, maxHP);
    }

    public void TakeDamage(int damage)
    {
        ApplyKnockback();

        currentHP -= damage;

        if (currentHP < 0)
            currentHP = 0;

        if (healthBar != null)
            healthBar.UpdateHealth(currentHP, maxHP);

        if (currentHP <= 0)
        {
            PlayerAudio audioPlayer = GetComponent<PlayerAudio>();

            if (audioPlayer != null)
                audioPlayer.PlayDeath();

            ANIMACAO_CONTROLE anim = GetComponent<ANIMACAO_CONTROLE>();

            if (anim != null)
                anim.PlayDeath();

            PlayerMovement movement = GetComponent<PlayerMovement>();

            if (movement != null)
                movement.Die();

            this.enabled = false;

            if (RunManager.Instance != null)
                Invoke(nameof(ResetRun), 3f);
            else
                Debug.LogError("RunManager.Instance não encontrado na cena.");
        }
        else
        {
            PlayerAudio audioPlayer = GetComponent<PlayerAudio>();

            if (audioPlayer != null)
                audioPlayer.PlayHurt();

            ANIMACAO_CONTROLE anim = GetComponent<ANIMACAO_CONTROLE>();

            if (anim != null)
                anim.PlayHurt();
        }
    }

    void ApplyKnockback()
    {
        if (rb == null) return;

        float dir = Mathf.Sign(transform.localScale.x);

        rb.velocity = new Vector2(0f, rb.velocity.y);

        rb.AddForce(
            new Vector2(-dir * knockbackForceX, knockbackForceY),
            ForceMode2D.Impulse
        );
    }

    void ResetRun()
    {
        if (RunManager.Instance != null)
            RunManager.Instance.ResetRun();
    }
}
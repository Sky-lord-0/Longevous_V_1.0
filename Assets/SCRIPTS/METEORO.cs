using System.Collections;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [Header("Fall")]
    public float fallSpeed = 10f;
    public int damage = 25;

    [Header("Animation")]
    public Animator animator;
    public float destroyDelayAfterHit = 0.4f;

    private bool hasHit = false;
    private Collider2D col;
    private EnemyAudio audioEnemy;

    void Awake()
    {
        col = GetComponent<Collider2D>();
        audioEnemy = FindObjectOfType<EnemyAudio>();

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Update()
    {
        if (hasHit) return;

        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit) return;

        // 💥 PLAYER → só toma dano, NÃO explode
        if (other.CompareTag("Player"))
        {
            DealDamage(other);
            return;
        }

        // 🌍 GROUND → explode + som
        if (other.CompareTag("Ground"))
        {
            StartCoroutine(Impact());
            return;
        }
    }

    void DealDamage(Collider2D other)
    {
        PlayerHealth ph = other.GetComponent<PlayerHealth>();

        if (ph != null)
            ph.TakeDamage(damage);
    }

    IEnumerator Impact()
    {
        hasHit = true;

        fallSpeed = 0f;

        if (col != null)
            col.enabled = false;

        // 🔊 SOM DO IMPACTO (AQUI É O LUGAR CERTO)
        if (audioEnemy != null && audioEnemy.meteorHitSound != null)
        {
            AudioSource.PlayClipAtPoint(
                audioEnemy.meteorHitSound,
                transform.position,
                1f
            );
        }

        // 💥 ANIMAÇÃO
        if (animator != null)
            animator.SetTrigger("explode");

        yield return new WaitForSeconds(destroyDelayAfterHit);

        Destroy(gameObject);
    }
}
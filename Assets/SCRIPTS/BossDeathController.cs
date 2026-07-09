using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDeathController : MonoBehaviour
{
    public EnemyHealth health;

    private Animator animator;
    private Rigidbody2D rb;
    private bool isDead = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (health == null)
            health = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (isDead) return;
        if (health == null) return;

        if (health.CurrentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        // Para toda a movimentação física
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        // Toca animação de morte
        if (animator != null)
        {
            animator.ResetTrigger("Hurt");
            animator.SetBool("isAscending", false);

            // Use Trigger se seu Animator usa Trigger
            animator.SetTrigger("isDead");

            // Se seu Animator usa Bool, troque pela linha abaixo:
            // animator.SetBool("isDead", true);
        }

        // Desativa todos os scripts do boss
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

        foreach (var s in scripts)
        {
            if (s != this)
                s.enabled = false;
        }

        StartCoroutine(DeathRoutine());
    }

    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("CUTSCENE_FINAL");
    }
}
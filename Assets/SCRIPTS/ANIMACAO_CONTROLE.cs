using UnityEngine;

public class ANIMACAO_CONTROLE : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAttack()
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Hurt");
        animator.SetTrigger("Attack");
    }

    public void PlayHurt()
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Hurt");
        animator.SetTrigger("Hurt");
    }

    public void PlayDeath()
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Hurt");
        animator.SetBool("isDead", true);
    }
}
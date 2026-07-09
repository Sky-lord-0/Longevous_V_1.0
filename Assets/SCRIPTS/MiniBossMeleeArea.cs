using UnityEngine;

public class MiniBossMeleeArea : MonoBehaviour
{
    public int damage = 35;
    private bool hasHitThisSwing = false;

    void OnEnable()
    {
        hasHitThisSwing = false;
    }

    private void TryHit(Collider2D other)
    {
        if (hasHitThisSwing) return;
        if (!other.CompareTag("Player")) return;

        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            ph.TakeDamage(damage);
            hasHitThisSwing = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryHit(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        TryHit(other);
    }
}
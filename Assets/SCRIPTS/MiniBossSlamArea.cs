using UnityEngine;

public class MiniBossSlamArea : MonoBehaviour
{
    public int damage = 30;

    private bool hasHitThisSlam = false;

    void OnEnable()
    {
        // Toda vez que o SlamArea liga, ele pode dar dano 1 vez
        hasHitThisSlam = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (hasHitThisSlam) return;
        if (!other.CompareTag("Player")) return;

        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            ph.TakeDamage(damage);
            hasHitThisSlam = true;
        }
    }
}
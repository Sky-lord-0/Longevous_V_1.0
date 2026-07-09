using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    public AudioClip attackSound;
    public AudioClip hurtSound;
    public AudioClip deathSound;

    [Header("Boss extras")]
    public AudioClip fireballSound;
    public AudioClip meteorSpawnSound;
    public AudioClip meteorHitSound;

    [Range(0f, 1f)] public float attackVolume = 1f;
    [Range(0f, 1f)] public float hurtVolume = 1f;
    [Range(0f, 1f)] public float deathVolume = 1f;

    [Range(0f, 1f)] public float fireballVolume = 1f;
    [Range(0f, 1f)] public float meteorVolume = 1f;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAttack()
    {
        if (attackSound != null)
            audioSource.PlayOneShot(attackSound, attackVolume);
    }

    public void PlayHurt()
    {
        if (hurtSound != null)
            audioSource.PlayOneShot(hurtSound, hurtVolume);
    }

    public void PlayDeath()
    {
        if (deathSound != null)
            audioSource.PlayOneShot(deathSound, deathVolume);
    }

    // 🔥 BOSS
    public void PlayFireball()
    {
        if (fireballSound != null)
            audioSource.PlayOneShot(fireballSound, fireballVolume);
    }

    public void PlayMeteorSpawn()
    {
        if (meteorSpawnSound != null)
            audioSource.PlayOneShot(meteorSpawnSound, meteorVolume);
    }

    public void PlayMeteorHit()
    {
        if (meteorHitSound != null)
            audioSource.PlayOneShot(meteorHitSound, meteorVolume);
    }
}
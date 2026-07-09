using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Player Sounds")]
    public AudioClip attackSound;
    public AudioClip jumpSound;
    public AudioClip dashSound;
    public AudioClip hurtSound;
    public AudioClip deathSound;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAttack()
    {
        audioSource.PlayOneShot(attackSound);
    }

    public void PlayJump()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayDash()
    {
        audioSource.PlayOneShot(dashSound);
    }

    public void PlayHurt()
    {
        audioSource.PlayOneShot(hurtSound);
    }

    public void PlayDeath()
    {
        audioSource.PlayOneShot(deathSound);
    }
}
using System.Collections;
using UnityEngine;

public class BossMage : MonoBehaviour
{
    [Header("Refs")]
    public Transform player;
    public EnemyHealth health;

    private SpriteRenderer sr;
    private Animator animator;
    private EnemyAudio audioEnemy;

    [Header("Fireball")]
    public GameObject fireballPrefab;
    public GameObject fireballRightPrefab;
    public GameObject fireballLeftPrefab;
    public Transform firePointRight;
    public Transform firePointLeft;
    public float attackCooldown = 2f;

    [Header("Teleport")]
    public float teleportChance = 30f;
    public float teleportCooldown = 2f;

    [Header("Float Movement")]
    public float floatSpeed = 2f;
    public float floatRange = 3f;

    [Header("Meteor")]
    public GameObject meteorPrefab;
    public float meteorDuration = 5f;
    public float meteorInterval = 0.4f;

    private Vector3 startPos;
    private float attackTimer;

    private bool canTeleport = true;
    private bool isFlying = false;
    private bool isDead = false;

    private int lastHP;

    void Start()
    {
        startPos = transform.position;

        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioEnemy = GetComponent<EnemyAudio>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        if (health == null)
            health = GetComponent<EnemyHealth>();

        lastHP = health.CurrentHP;

        StartCoroutine(MeteorRoutine());
    }

    void Update()
    {
        if (isDead) return;
        if (health == null || player == null) return;

        CheckDamage();

        if (!isFlying)
        {
            FloatMovement();

            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCooldown)
            {
                attackTimer = 0f;
                FireballAttack();
            }
        }
    }

    void LateUpdate()
    {
        if (isDead || player == null || sr == null) return;

        LookAtPlayer();
    }

    void LookAtPlayer()
    {
        sr.flipX = player.position.x < transform.position.x;
    }

    void FireballAttack()
    {
        if (player == null) return;

        float hpPercent = (float)health.CurrentHP / health.maxHP;

        int amount = 1;
        if (hpPercent <= 0.6f) amount = 3;
        if (hpPercent <= 0.3f) amount = 5;

        float spread = 25f;

        Transform chosenFirePoint = (player.position.x < transform.position.x)
            ? firePointLeft
            : firePointRight;

        Vector2 baseDir = (chosenFirePoint == firePointLeft) ? Vector2.left : Vector2.right;

        for (int i = 0; i < amount; i++)
        {
            float angle = (i - (amount - 1) / 2f) * spread;
            Vector2 finalDir = Quaternion.Euler(0, 0, angle) * baseDir;

            GameObject prefabToUse = (chosenFirePoint == firePointLeft)
                ? fireballLeftPrefab
                : fireballRightPrefab;

            GameObject fb = Instantiate(
                prefabToUse,
                chosenFirePoint.position,
                Quaternion.identity
            );

            fb.GetComponent<Fireball>().dir = finalDir.normalized;
        }

        // 🔊 SOM FIREBALL
        audioEnemy?.PlayFireball();
    }

    void FloatMovement()
    {
        float offset = Mathf.Sin(Time.time * floatSpeed) * floatRange;

        transform.position = new Vector3(
            startPos.x + offset,
            startPos.y,
            startPos.z
        );
    }

    void CheckDamage()
    {
        if (health.CurrentHP < lastHP)
        {
            OnTakeDamage();
            lastHP = health.CurrentHP;
        }
    }

    void OnTakeDamage()
    {
        if (canTeleport)
        {
            if (Random.Range(0f, 100f) <= teleportChance)
                StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        canTeleport = false;

        float offset = Random.Range(-4f, 4f);

        transform.position = new Vector3(
            player.position.x + offset,
            transform.position.y,
            transform.position.z
        );

        yield return new WaitForSeconds(teleportCooldown);

        canTeleport = true;
    }

    IEnumerator MeteorRoutine()
    {
        while (!isDead && health != null && health.CurrentHP > 0)
        {
            yield return new WaitForSeconds(12f);

            yield return StartCoroutine(FlyUp());

            float timer = 0f;

            while (timer < meteorDuration)
            {
                SpawnMeteor();
                timer += meteorInterval;
                yield return new WaitForSeconds(meteorInterval);
            }

            yield return StartCoroutine(FlyDown());
        }
    }

    IEnumerator FlyUp()
    {
        isFlying = true;

        if (animator != null)
            animator.SetBool("isAscending", true);

        Vector3 target = startPos + Vector3.up * 4f;
        Vector3 start = transform.position;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }
    }

    IEnumerator FlyDown()
    {
        Vector3 target = startPos;
        Vector3 start = transform.position;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }

        if (animator != null)
            animator.SetBool("isAscending", false);

        isFlying = false;
    }

    void SpawnMeteor()
    {
        if (meteorPrefab == null || player == null) return;

        Vector3 pos = new Vector3(
            player.position.x + Random.Range(-3f, 3f),
            player.position.y + 8f,
            0f
        );

        Instantiate(meteorPrefab, pos, Quaternion.identity);

        // 🔊 SOM RAIO CAINDO
        audioEnemy?.PlayMeteorSpawn();
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;

        StopAllCoroutines();

        if (animator != null)
            animator.SetBool("isDead", true);

        // 🔊 SOM MORTE (caso não esteja no EnemyHealth)
        audioEnemy?.PlayDeath();

        this.enabled = false;

        Destroy(gameObject, 3f);
    }
}
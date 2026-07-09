using System.Collections;
using UnityEngine;

public class MiniBossController : MonoBehaviour
{
    private enum State { Chase, Windup, Attack, SlamWindup, Slam }

    [Header("Stats")]
    public int damageMelee = 35;
    public int damageSlam = 30;

    [Header("Ranges (medidos por X/Y - não centro)")]
    public float chaseRangeX = 10f;
    public float meleeRangeX = 2.2f;
    public float meleeRangeY = 1.2f;
    public float slamRangeX = 2.6f;
    public float slamRangeY = 1.6f;

    [Header("Movement")]
    public float chaseSpeedPhase1 = 3.0f;
    public float chaseSpeedPhase2 = 4.2f;

    [Header("Cooldowns")]
    public float meleeCooldown = 1.2f;
    public float slamCooldown = 2.2f;

    [Header("Telegraph")]
    public float windupTime = 0.25f;
    public float slamWindupTime = 0.35f;

    [Header("Refs")]
    public GameObject meleeAreaObject; // MeleeArea (desativado por padrão)
    public GameObject slamAreaObject;  // SlamArea (desativado por padrão)
    public SpriteRenderer sr;

    private Rigidbody2D rb;
    private Transform player;
    private EnemyHealth health;

    private State state = State.Chase;
    private float lastMeleeTime = -999f;
    private float lastSlamTime = -999f;
    private bool phase2 = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        health = GetComponent<EnemyHealth>();

        if (sr == null) sr = GetComponent<SpriteRenderer>();

        if (meleeAreaObject == null)
        {
            Transform t = transform.Find("MeleeArea");
            if (t != null) meleeAreaObject = t.gameObject;
        }

        if (slamAreaObject == null)
        {
            Transform t = transform.Find("SlamArea");
            if (t != null) slamAreaObject = t.gameObject;
        }

        if (meleeAreaObject != null) meleeAreaObject.SetActive(false);
        if (slamAreaObject != null) slamAreaObject.SetActive(false);

        // garante dano correto nos scripts
        if (meleeAreaObject != null)
        {
            var m = meleeAreaObject.GetComponent<MiniBossMeleeArea>();
            if (m != null) m.damage = damageMelee;
        }
        if (slamAreaObject != null)
        {
            var s = slamAreaObject.GetComponent<MiniBossSlamArea>();
            if (s != null) s.damage = damageSlam;
        }
    }

    void Update()
    {
        if (player == null || health == null) return;

        if (!phase2 && HealthPercent() <= 0.5f)
            phase2 = true;

        if (state != State.Chase) return;

        float dx = Mathf.Abs(player.position.x - transform.position.x);
        float dy = Mathf.Abs(player.position.y - transform.position.y);

        // MELEE (fase 1 e 2) — prioridade se estiver perto
        if (dx <= meleeRangeX && dy <= meleeRangeY && Time.time - lastMeleeTime >= meleeCooldown)
        {
            StartCoroutine(MeleeRoutine());
            return;
        }

        // SLAM (somente fase 2)
        if (phase2 && dx <= slamRangeX && dy <= slamRangeY && Time.time - lastSlamTime >= slamCooldown)
        {
            StartCoroutine(SlamRoutine());
            return;
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        if (state == State.Chase)
        {
            float dx = Mathf.Abs(player.position.x - transform.position.x);
            if (dx > chaseRangeX)
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
                return;
            }

            float speed = phase2 ? chaseSpeedPhase2 : chaseSpeedPhase1;
            float dir = Mathf.Sign(player.position.x - transform.position.x);

            rb.velocity = new Vector2(dir * speed, rb.velocity.y);

            if (sr != null)
                sr.flipX = (dir < 0);
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    IEnumerator MeleeRoutine()
    {
        state = State.Windup;
        lastMeleeTime = Time.time;

        // telegraph: pisca vermelho (fase 1 e 2)
        if (sr != null) sr.color = new Color(1f, 0.6f, 0.6f, 1f);
        yield return new WaitForSeconds(windupTime);
        if (sr != null) sr.color = Color.white;

        state = State.Attack;

        if (meleeAreaObject != null)
            meleeAreaObject.SetActive(true);

        yield return new WaitForSeconds(0.10f);

        if (meleeAreaObject != null)
            meleeAreaObject.SetActive(false);

        state = State.Chase;
    }

    IEnumerator SlamRoutine()
    {
        state = State.SlamWindup;
        lastSlamTime = Time.time;

        if (sr != null) sr.color = new Color(1f, 0.4f, 0.4f, 1f);
        yield return new WaitForSeconds(slamWindupTime);
        if (sr != null) sr.color = Color.white;

        state = State.Slam;

        if (slamAreaObject != null)
            slamAreaObject.SetActive(true);

        yield return new WaitForSeconds(0.10f);

        if (slamAreaObject != null)
            slamAreaObject.SetActive(false);

        state = State.Chase;
    }

    float HealthPercent()
    {
        if (health.maxHP <= 0) return 1f;
        return (float)health.CurrentHP / (float)health.maxHP;
    }
}
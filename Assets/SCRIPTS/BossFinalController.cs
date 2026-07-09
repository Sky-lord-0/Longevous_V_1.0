using System.Collections;
using UnityEngine;

public class BossFinalController : MonoBehaviour
{
    private enum State { Chase, Windup, Attack, SlamWindup, Slam }

    [Header("Stats")]
    public int damageMelee = 40;
    public int damageSlam = 35;

    [Header("Ranges (X/Y)")]
    public float chaseRangeX = 14f;
    public float meleeRangeX = 2.4f;
    public float meleeRangeY = 1.4f;
    public float slamRangeX = 3.0f;
    public float slamRangeY = 1.8f;

    [Header("Movement")]
    public float speedPhase1 = 3.2f;
    public float speedPhase2 = 4.0f;
    public float speedPhase3 = 4.8f;

    [Header("Cooldowns")]
    public float meleeCooldownP1 = 1.1f;
    public float slamCooldownP1 = 2.2f;

    public float meleeCooldownP2 = 0.95f;
    public float slamCooldownP2 = 1.9f;

    public float meleeCooldownP3 = 0.80f;
    public float slamCooldownP3 = 1.6f;

    [Header("Telegraph")]
    public float windupTime = 0.22f;
    public float slamWindupTime = 0.32f;

    [Header("Refs")]
    public GameObject meleeAreaObject; // filho MeleeArea (trigger)
    public GameObject slamAreaObject;  // filho SlamArea (trigger)
    public SpriteRenderer sr;

    private Rigidbody2D rb;
    private Transform player;
    private EnemyHealth health;

    private State state = State.Chase;
    private float lastMeleeTime = -999f;
    private float lastSlamTime = -999f;

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

        // aplica dano correto nos scripts das áreas
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
        if (state != State.Chase) return;

        float dx = Mathf.Abs(player.position.x - transform.position.x);
        float dy = Mathf.Abs(player.position.y - transform.position.y);

        float meleeCd = GetMeleeCooldown();
        float slamCd = GetSlamCooldown();

        // prioridade: melee se perto
        if (dx <= meleeRangeX && dy <= meleeRangeY && Time.time - lastMeleeTime >= meleeCd)
        {
            StartCoroutine(MeleeRoutine());
            return;
        }

        // slam se perto (todas as fases, mas fica mais frequente)
        if (dx <= slamRangeX && dy <= slamRangeY && Time.time - lastSlamTime >= slamCd)
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

            float dir = Mathf.Sign(player.position.x - transform.position.x);
            float speed = GetMoveSpeed();

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

        yield return new WaitForSeconds(0.12f);

        if (slamAreaObject != null)
            slamAreaObject.SetActive(false);

        state = State.Chase;
    }

    float HpPercent()
    {
        if (health.maxHP <= 0) return 1f;
        return (float)health.CurrentHP / (float)health.maxHP;
    }

    int Phase()
    {
        float p = HpPercent();
        if (p > 0.67f) return 1;
        if (p > 0.34f) return 2;
        return 3;
    }

    float GetMoveSpeed()
    {
        int ph = Phase();
        if (ph == 1) return speedPhase1;
        if (ph == 2) return speedPhase2;
        return speedPhase3;
    }

    float GetMeleeCooldown()
    {
        int ph = Phase();
        if (ph == 1) return meleeCooldownP1;
        if (ph == 2) return meleeCooldownP2;
        return meleeCooldownP3;
    }

    float GetSlamCooldown()
    {
        int ph = Phase();
        if (ph == 1) return slamCooldownP1;
        if (ph == 2) return slamCooldownP2;
        return slamCooldownP3;
    }
}
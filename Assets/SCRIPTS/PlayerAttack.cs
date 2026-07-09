using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Combo 3 hits (fixo)")]
    public float timeBetweenHits = 0.20f;   // intervalo entre hits
    public float comboResetTime = 0.35f;    // se passar disso sem atacar, volta pro hit 1
    public float hitboxActiveTime = 0.08f;  // quanto tempo a hitbox fica ligada

    [Header("Timing")]
    public float attackDelay = 0.15f;
    
    [Header("Damage")]
    public int damagePerHit = 30;

    [Header("Hitstop")]
    public float hitstopDuration = 0.07f;   // 0.06–0.08

    [Header("Refs")]
    public GameObject attackHitboxObject;
    public AttackHitbox attackHitbox;

    private int comboIndex = 0;             // 0,1,2
    private float lastAttackTime = -999f;
    private bool isAttacking = false;

    void Awake()
    {
        // Se esquecer de linkar, tenta achar automaticamente
        if (attackHitboxObject == null)
        {
            Transform t = transform.Find("AttackHitbox");
            if (t != null) attackHitboxObject = t.gameObject;
        }

        if (attackHitbox == null && attackHitboxObject != null)
        {
            attackHitbox = attackHitboxObject.GetComponent<AttackHitbox>();
        }

        if (attackHitboxObject != null)
            attackHitboxObject.SetActive(false);

        if (attackHitbox != null)
            attackHitbox.SetDamage(damagePerHit, hitstopDuration);
    }

    void Update()
    {
        // Reset do combo se demorou demais
        if (Time.time - lastAttackTime > comboResetTime)
            comboIndex = 0;

        // Ataque no clique esquerdo (Fire1)
        if (Input.GetButtonDown("Fire1"))
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        if (isAttacking) return;

        // NÃO ataca enquanto defende (botão direito segurado)
        PlayerDefense defense = GetComponent<PlayerDefense>();
        if (defense != null && defense.isDefending) return;

        // Intervalo entre hits
        if (Time.time - lastAttackTime < timeBetweenHits) return;

        lastAttackTime = Time.time;
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
    isAttacking = true;

    ANIMACAO_CONTROLE anim = GetComponent<ANIMACAO_CONTROLE>();

    if (anim != null)
        anim.PlayAttack();

    PlayerAudio audioPlayer = GetComponent<PlayerAudio>();

    if (audioPlayer != null)
        audioPlayer.PlayAttack();
    // Espera a animação chegar no frame do golpe
    yield return new WaitForSecondsRealtime(attackDelay);

    // Atualiza dano/hitstop
    if (attackHitbox != null)
        attackHitbox.SetDamage(damagePerHit, hitstopDuration);

    // Liga hitbox
    if (attackHitboxObject != null)
        attackHitboxObject.SetActive(true);

    yield return new WaitForSecondsRealtime(hitboxActiveTime);

    // Desliga hitbox
    if (attackHitboxObject != null)
        attackHitboxObject.SetActive(false);

    comboIndex++;
    if (comboIndex > 2)
        comboIndex = 0;

    isAttacking = false;
    }
}
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 8f;
    public int damage = 20;
    public float lifeTime = 5f;

    [HideInInspector]
    public Vector2 dir;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void Init(Vector2 direction)
    {
        dir = direction.normalized;

        // 🔥 ajusta visual da fireball
        if (sr != null)
        {
            sr.flipX = dir.x < 0;
        }

        // opcional (caso sprite precise rotacionar também)
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);

        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.simulated = true;
        }
    }

    void Update()
    {
        transform.position += (Vector3)(dir.normalized * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth p = other.GetComponent<PlayerHealth>();
            if (p != null)
                p.TakeDamage(damage);

            Destroy(gameObject);
        }

        if (!other.isTrigger && !other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
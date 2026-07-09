using UnityEngine;

public class SUBIRESCADA : MonoBehaviour
{
    public float climbSpeed = 5f;

    private bool isOnLadder = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isOnLadder)
        {
            rb.gravityScale = 0;

            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("Subindo");
                rb.velocity = new Vector2(rb.velocity.x, climbSpeed);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
        else
        {
            rb.gravityScale = 3;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            Debug.Log("Entrou na escada");
            isOnLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            Debug.Log("Saiu da escada");
            isOnLadder = false;
        }
    }
}
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public float floatUpSpeed = 1.8f;
    public float lifeTime = 0.6f;

    private TMP_Text text;
    private float endTime;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    public void SetText(string s)
    {
        if (text != null) text.text = s;
    }

    void OnEnable()
    {
        endTime = Time.unscaledTime + lifeTime;
    }

    void Update()
    {
        transform.position += Vector3.up * floatUpSpeed * Time.unscaledDeltaTime;

        if (Time.unscaledTime >= endTime)
            Destroy(gameObject);
    }
}
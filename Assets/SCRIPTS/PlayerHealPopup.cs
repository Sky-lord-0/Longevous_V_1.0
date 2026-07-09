using UnityEngine;

public class PlayerHealPopup : MonoBehaviour
{
    public GameObject popupPrefab; // use o DamageText prefab
    public float yOffset = 1.0f;

    public void ShowHeal(int amount)
    {
        if (popupPrefab == null) return;

        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null) return;

        GameObject go = Instantiate(popupPrefab, canvas.transform);

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * yOffset);
        go.transform.position = screenPos;

        DamagePopup dp = go.GetComponent<DamagePopup>();
        if (dp != null)
            dp.SetText("+" + amount);
    }
}
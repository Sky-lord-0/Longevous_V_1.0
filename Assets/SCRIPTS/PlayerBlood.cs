using UnityEngine;

public class PlayerBlood : MonoBehaviour
{
    public int currentBlood = 0;

    [Header("Healing")]
    public int healAmount = 40;

    // Escalonamento
    public bool dangerLevel1; // 80+
    public bool dangerLevel2; // 150+
    public bool dangerLevel3; // 220+

    public void AddBlood(int amount)
    {
        currentBlood += amount;
        UpdateDangerLevels();
    }

    public void SpendBloodToHeal(PlayerHealth health)
    {
        if (currentBlood < healAmount) return;

        currentBlood -= healAmount;
        health.currentHP = Mathf.Min(
    health.currentHP + healAmount,
    health.maxHP
);

var healPopup = GetComponent<PlayerHealPopup>();
if (healPopup != null)
    healPopup.ShowHeal(healAmount);

UpdateDangerLevels();
    }

    void UpdateDangerLevels()
    {
        dangerLevel1 = currentBlood >= 80;
        dangerLevel2 = currentBlood >= 150;
        dangerLevel3 = currentBlood >= 220;
    }

    void Update()
{
    if (Input.GetKeyDown(KeyCode.C))
    {
        PlayerHealth ph = GetComponent<PlayerHealth>();
        if (ph != null)
            SpendBloodToHeal(ph);
    }
}
}
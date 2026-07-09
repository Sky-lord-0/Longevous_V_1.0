using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [Header("Upgrade flags")]
    public bool burnOnHit = false;
    public bool lifestealOnKill = false;
    public bool critChance = false;

    [Header("Numbers")]
    public float critPercent = 15f;          // 15% padrão
    public float critMultiplier = 2f;        // dano x2
    public float lifestealPercent = 0.03f;   // 3% da vida máxima por kill (exemplo seu)

    public void ApplyUpgrade(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.BurnOnHit:
                burnOnHit = true;
                break;

            case UpgradeType.LifestealOnKill:
                lifestealOnKill = true;
                break;

            case UpgradeType.CritChance:
                critChance = true;
                break;
        }
    }
}
using UnityEngine;

public class RewardOnDeath : MonoBehaviour
{
    private UpgradeUI upgradeUI;

    void Awake()
    {
        upgradeUI = FindObjectOfType<UpgradeUI>();
    }

    public void GiveReward()
    {
        if (upgradeUI != null)
            upgradeUI.ShowThreeOptions();
    }
}
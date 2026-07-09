using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    public GameObject panel;
    public Button b1, b2, b3;
    public TMP_Text t1, t2, t3;

    private PlayerUpgrades playerUpgrades;
    private UpgradeType[] currentOptions;

    void Awake()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) playerUpgrades = p.GetComponent<PlayerUpgrades>();

        if (panel != null) panel.SetActive(false);
    }

    public void ShowThreeOptions()
    {
        if (playerUpgrades == null) return;

        // pausa
        Time.timeScale = 0f;

        // 3 opções fixas por enquanto (rápido e sem bug)
        currentOptions = new UpgradeType[]
        {
            UpgradeType.BurnOnHit,
            UpgradeType.LifestealOnKill,
            UpgradeType.CritChance
        };

        if (panel != null) panel.SetActive(true);

        SetButton(0, b1, t1);
        SetButton(1, b2, t2);
        SetButton(2, b3, t3);

        b1.onClick.RemoveAllListeners();
        b2.onClick.RemoveAllListeners();
        b3.onClick.RemoveAllListeners();

        b1.onClick.AddListener(() => Choose(0));
        b2.onClick.AddListener(() => Choose(1));
        b3.onClick.AddListener(() => Choose(2));
    }

    void SetButton(int index, Button b, TMP_Text t)
    {
        UpgradeType type = currentOptions[index];
        if (t != null) t.text = GetUpgradeName(type);
        b.interactable = true;
    }

    string GetUpgradeName(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.BurnOnHit: return " Ataques aplicam fogo";
            case UpgradeType.LifestealOnKill: return " Cura 3% da vida máxima por kill";
            case UpgradeType.CritChance: return " Chance de crítico (x2 dano)";
            default: return type.ToString();
        }
    }

    void Choose(int index)
    {
        UpgradeType chosen = currentOptions[index];
        playerUpgrades.ApplyUpgrade(chosen);

        if (panel != null) panel.SetActive(false);

        // despausa
        Time.timeScale = 1f;
    }
}
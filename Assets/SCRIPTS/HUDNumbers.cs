using UnityEngine;
using TMPro;

public class HUDNumbers : MonoBehaviour
{
    public TMP_Text hpText;
    public TMP_Text bloodText;

    private PlayerHealth ph;
    private PlayerBlood pb;

    void Awake()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            ph = p.GetComponent<PlayerHealth>();
            pb = p.GetComponent<PlayerBlood>();
        }
    }

    void Update()
    {
        if (ph != null && hpText != null)
            hpText.text = $"HP: {ph.currentHP} / {ph.maxHP}";

        if (pb != null && bloodText != null)
            bloodText.text = $"Sangue: {pb.currentBlood}";
    }
}
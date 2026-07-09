using UnityEngine;

public class PlayerDefense : MonoBehaviour
{
    public bool isDefending = false;

    void Update()
    {
        // Botão direito do mouse segurado
        if (Input.GetMouseButtonDown(1)) isDefending = true;
        if (Input.GetMouseButtonUp(1)) isDefending = false;
    }

    // Se defendendo, bloqueia 100% do dano
    public int ModifyDamage(int incomingDamage)
    {
        return isDefending ? 0 : incomingDamage;
    }
}
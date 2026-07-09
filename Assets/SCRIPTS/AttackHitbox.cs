using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private int damage;
    private float hitstop;

    // evita bater várias vezes no mesmo inimigo durante o mesmo ataque
    private HashSet<EnemyHealth> hitEnemies = new HashSet<EnemyHealth>();

    public void SetDamage(int dmg, float hitstopDuration)
    {
        damage = dmg;
        hitstop = hitstopDuration;
    }

    void OnEnable()
    {
        // limpa lista toda vez que ativa a hitbox
        hitEnemies.Clear();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        EnemyHealth enemy = col.GetComponent<EnemyHealth>();

        if (enemy == null) return;

        // impede multi-hit no mesmo ataque
        if (hitEnemies.Contains(enemy)) return;

        hitEnemies.Add(enemy);

        enemy.TakeDamage(damage, transform.position);

        // (opcional futuro) hitstop pode ser usado aqui depois
        // Time.timeScale = 0.05f;
    }
}
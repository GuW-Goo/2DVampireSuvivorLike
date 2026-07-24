using System.Collections.Generic;
using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    private float damage;
    private float knockbackForce;
    private Transform attackerTransform;

    private HashSet<Monster> hitMonsters = new();

    public void SetUp(float weaponDamage, float knockback, Transform attacker)
    {
        damage = weaponDamage;
        knockbackForce = knockback;
        attackerTransform = attacker;
        hitMonsters.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Monster>(out Monster monster))
        {
            if(!hitMonsters.Contains(monster))
            {
                hitMonsters.Add(monster);
                monster.TakeDamage(damage, knockbackForce, attackerTransform.position);
            }
        }
    }
}

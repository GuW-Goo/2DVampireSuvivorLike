using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage, float knockbackForce, Vector2 hitPosition);
}

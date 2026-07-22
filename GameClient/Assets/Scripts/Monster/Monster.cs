using System;
using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{
    [SerializeField] private MonsterStatSO statSO;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float currentMaxHP;
    private float currentHP;
    private float currentDamage;

    public event Action<Monster> OnDeath;
    public MonsterStatSO StatSO => statSO;

    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void InitMonster(float hpMultiplier = 1.0f, float damageMultiplier = 1.0f)
    {
        currentMaxHP = statSO.MaxHP * hpMultiplier;
        currentHP = currentMaxHP;
        currentDamage = statSO.Damage * damageMultiplier;

        gameObject.SetActive(true);
    }

    public void Retarget(Vector2 targetPosition)
    {
        // 몬스터가 목표를 재설정 하는 로직
    }

    public void TakeDamage(float damage, float knockbackForce, Vector2 hitPosition)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Die();
            return;
        }

        // 넉백 로직 추후 추가
    }

    private void Die()
    {
        // 경험치나 보상 로직 추후 추가

        if (rb != null) rb.linearVelocity = Vector2.zero;
        OnDeath?.Invoke(this);
    }
}

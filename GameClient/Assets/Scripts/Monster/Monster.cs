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
    private Transform targetTransform;

    public event Action<Monster> OnDeath;
    public MonsterStatSO StatSO => statSO;

    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void InitMonster(Vector2 spawnPosition ,float hpMultiplier = 1.0f, float damageMultiplier = 1.0f)
    {
        currentMaxHP = statSO.MaxHP * hpMultiplier;
        currentHP = currentMaxHP;
        currentDamage = statSO.Damage * damageMultiplier;

        rb.position = spawnPosition;
        rb.linearVelocity = Vector2.zero;

        gameObject.SetActive(true);
    }

    public void SetTarget(Transform target)
    {
        targetTransform = target;
    }

    public void MoveToTarget(float deltaTime)
    {
        if(targetTransform == null) return;

        Vector2 dir = (targetTransform.position - transform.position).normalized;
        Vector2 movePosition = rb.position + dir * (statSO.MoveSpeed * deltaTime);
        rb.MovePosition(movePosition);
    }

    public void TakeDamage(float damage, float knockbackForce, Vector2 attackerPosition)
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

        rb.linearVelocity = Vector2.zero;
        OnDeath?.Invoke(this);
    }
}

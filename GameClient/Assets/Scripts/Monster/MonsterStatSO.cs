using UnityEngine;

[CreateAssetMenu(fileName = "MonsterStatSO", menuName = "Scriptable Objects/MonsterStatSO")]
public class MonsterStatSO : ScriptableObject
{
    [Header("몬스터의 스탯")]
    [SerializeField] private float maxHP = 5.0f;
    [SerializeField] private float damage = 1.0f;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float expAmount = 1.0f;
    [SerializeField] private float knockbackResist = 1.0f;

    [Header("몬스터 프리팹")]
    [SerializeField] private GameObject monsterPrefab;

    public float MaxHP => maxHP;
    public float Damage => damage;
    public float MoveSpeed => moveSpeed;
    public float ExpAmount => expAmount;
    public float KnockbackResist => knockbackResist;
    public GameObject MonsterPrefab => monsterPrefab;
}

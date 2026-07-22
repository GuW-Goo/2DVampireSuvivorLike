using UnityEngine;

// 무기 매커니즘 유형 정의
public enum WeaponType
{
    Melee,          // 근처 적에게 휘두르는 근거리 공격 (칼, 채찍 등)
    Aura,           // 플레이어 중심의 지속 틱데미지 오라
    Orbit,          // 지속적으로 주변을 도는 무기
    Projectile,     // 적을 향해 일직선으로 발사되는 무기 (총, 활 등)
    Spread,         // 방사형으로 퍼져나가는 무기 (샷건 등)
    AreaOfEffect    // 특정 지점에 투척하여 범위 피해를 주는 무기 (폭탄 등)
}


[CreateAssetMenu(fileName = "PlayerWeaponsSO", menuName = "Scriptable Objects/PlayerWeaponsSO")]
public class PlayerWeaponsSO : ScriptableObject
{
    [Header("기본 정보")]
    [SerializeField] private string weaponName;
    [SerializeField] private WeaponType weaponType;
    [TextArea]
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;
    [SerializeField] private GameObject weaponPrefab;   // 생성할 발사체 or 무기 프리팹

    [Header("공통 전투 스탯")]
    [SerializeField] private float damage = 10.0f;
    [SerializeField] private float cooldown = 1.5f;         // 공격 간격
    [SerializeField] private float area = 1.0f;             // 공격 범위
    [SerializeField] private float knockbackForce = 2.0f;
    [SerializeField] private float criticalChance = 0.05f; // 5%

    [Header("원거리 투사체 / 방사형 / 폭탄 옵션 (Projectile, Spread, AreaOfEffect)")]
    [SerializeField] private float projectileSpeed = 8.0f;  // 발사체 속도
    [SerializeField] private int projectileCount = 1;       // 발사체 갯수
    [SerializeField] private int pierceCount = 1;           // 관통 횟수 (0 = 첫 타격 후 소멸)
    [SerializeField] private float lifeTime = 3.0f;         // 발사체 유지 시간
    [SerializeField] private float explosionRadius = 0f;    // 폭발 범위 (AreaOfEffect용)
    [SerializeField] private float spreadAngle = 30.0f;     // 방사형 무기 퍼짐 각도

    [Header("근거리 공격 / 회전 / 오라 옵션 (Orbit, Melee, Aura)")]
    [SerializeField] private float rotateSpeed = 150.0f;    // 회전 속도 (Orbit 회전 속도, Melee 휘두르는 속도)
    [SerializeField] private float orbitRadius = 2.0f;      // 회전 반경 (Orbit 플레이어와의 거리)
    [SerializeField] private float tickInterval = 0.5f;     // Aura 틱 데미지 주기
    [SerializeField] private float attackAngle = 90.0f;     // Melee 궤적 각도

    // Getters
    public string WeaponName => weaponName;
    public WeaponType WeaponType => weaponType;
    public string Description => description;
    public Sprite Icon => icon;
    public GameObject WeaponPrefab => weaponPrefab;

    public float Damage => damage;
    public float Cooldown => cooldown;
    public float Area => area;
    public float KnockbackForce => knockbackForce;
    public float CriticalChance => criticalChance;

    public float ProjectileSpeed => projectileSpeed;
    public int ProjectileCount => projectileCount;
    public int PierceCount => pierceCount;
    public float LifeTime => lifeTime;
    public float ExplosionRadius => explosionRadius;
    public float SpreadAngle => spreadAngle;

    public float RotateSpeed => rotateSpeed;
    public float OrbitRadius => orbitRadius;
    public float TickInterval => tickInterval;
    public float AttackAngle => attackAngle;
}

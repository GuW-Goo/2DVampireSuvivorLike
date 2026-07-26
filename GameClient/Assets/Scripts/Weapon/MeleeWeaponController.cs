using System.Collections;
using UnityEngine;

public class MeleeWeaponController : BaseWeaponController
{
    // 적 몬스터 탐색범위 보정
    [SerializeField] private float extraRange = 5.0f;

    protected override void Attack()
    {
        float searchRadius = runtimeWeapon.weaponsData.Area + extraRange;
        Monster target = MonsterManager.Instance.GetNearestMonster(playerTransform.position, searchRadius);

        if (target == null) return;
        if (spawnedWeaponPrefab == null) return;

        MeleeHitbox meleeHitbox = spawnedWeaponPrefab.GetComponentInChildren<MeleeHitbox>(true);

        if (meleeHitbox != null)
        {
            Vector2 attackDir = (target.transform.position - playerTransform.position).normalized;

            float damage = runtimeWeapon.weaponsData.Damage;
            float knockback = runtimeWeapon.weaponsData.KnockbackForce;

            meleeHitbox.SetUp(damage, knockback, playerTransform);

            StartCoroutine(SwingRoutine(attackDir));
        }

    }

    IEnumerator SwingRoutine(Vector2 attackDir)
    {
        float attackAngle = runtimeWeapon.weaponsData.AttackAngle;

        float baseAngle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg;

        float startAngle = baseAngle - (attackAngle / 2.0f);
        float endAngle = baseAngle + (attackAngle / 2.0f);

        float elapsed = 0.0f;
        float duration = 0.05f;

        spawnedWeaponPrefab.SetActive(true);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / duration);

            float currentAngle = (float)Mathf.Lerp(startAngle, endAngle, progress);

            transform.localPosition = Vector2.zero;
            transform.localRotation = Quaternion.Euler(0.0f, 0.0f, currentAngle);

            yield return null;
        }

        spawnedWeaponPrefab.gameObject.SetActive(false);
    }
}

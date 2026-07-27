using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class OrbitWeaponController : BaseWeaponController
{
    private List<GameObject> activeOrbiters = new();
    private int currentOrbiterCount = 0;

    public override void Init(RuntimeWeapon weapon, Transform player)
    {
        base.Init(weapon, player);

        if (spawnedWeaponPrefab != null)
        {
            spawnedWeaponPrefab.SetActive(false);
        }

        int initialCount = GetCountByLevel(runtimeWeapon.currentLevel);
        RefreshOrbiters(initialCount);
    }

    private void Update()
    {
        if (runtimeWeapon == null) return;

        transform.position = playerTransform.position;

        float speed = runtimeWeapon.weaponsData.RotateSpeed;
        transform.Rotate(0.0f, 0.0f, speed * Time.deltaTime);

        int targetCount = GetCountByLevel(runtimeWeapon.currentLevel);
        if(targetCount != currentOrbiterCount)
        {
            RefreshOrbiters(targetCount);
        }
    }

    protected override void Attack()
    {
        // Todo: 공격로직 추가
    }

    private int GetCountByLevel(int currentlevel)
    {
        var levels = runtimeWeapon.weaponsData.CountIncreaseLevels;
        int maxLimit = runtimeWeapon.weaponsData.MaxOrbiterCount;

        if (levels == null || levels.Count == 0) return 1;

        int count = levels.Count(lvl => currentlevel >= lvl);

        return Mathf.Clamp(count, 1, maxLimit);
    }


    private void RefreshOrbiters(int count)
    {
        float radius = runtimeWeapon.weaponsData.OrbitRadius;
        GameObject prefab = runtimeWeapon.weaponsData.WeaponPrefab;

        if (prefab == null) return;

        currentOrbiterCount = count;

        // 필요한 개수보다 모자라면 추가로 생성
        while(activeOrbiters.Count < count)
        {
            GameObject newOrbiter = Instantiate(prefab, transform);
            newOrbiter.SetActive(false);
            activeOrbiters.Add(newOrbiter);
        }

        // 360도를 갯수만큼 균등 분할
        float angleStep = count > 0 ? 360.0f / count : 0.0f;

        for(int i = 0; i < activeOrbiters.Count; i++)
        {
            GameObject orbiter = activeOrbiters[i];

            if(i < count)
            {
                float currentAngle = i * angleStep * Mathf.Deg2Rad;
                Vector2 spawnPos = new Vector2(
                    Mathf.Cos(currentAngle) * radius, 
                    Mathf.Sin(currentAngle) * radius
                );

                orbiter.transform.localPosition = spawnPos;
                orbiter.transform.localRotation = Quaternion.identity;
                orbiter.SetActive(true);

                MeleeHitbox hitbox = orbiter.GetComponent<MeleeHitbox>();
                if (hitbox != null)
                {
                    hitbox.SetUp(runtimeWeapon.GetCalculateDamage(), runtimeWeapon.weaponsData.KnockbackForce, playerTransform);
                }
            }
            else
            {
                // 필요갯수 초과시 남는 공전체 비활성화
                orbiter.SetActive(false);
            }

        }
    }


}

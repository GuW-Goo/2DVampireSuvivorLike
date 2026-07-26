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

        RefreshOrbiters();
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
            RefreshOrbiters();
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

        return Mathf.Min(count, maxLimit);
    }


    private void RefreshOrbiters()
    {
        // Todo: Orbit의 공전궤도 계산 로직 추가
    }


}

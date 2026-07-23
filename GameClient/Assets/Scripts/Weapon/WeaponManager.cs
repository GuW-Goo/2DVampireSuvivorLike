using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }

    [Header("전체 무기 SO 목록")]
    [SerializeField] private List<PlayerWeaponsSO> allWeaponSOList;

    [Header("현재 장착된 무기들")]
    private List<RuntimeWeapon> activeWaepons = new();

    [SerializeField] private Transform playerTransform;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public List<PlayerWeaponsSO> GetRandomWeaponChoices(int count = 3)
    {
        List<PlayerWeaponsSO> candidates = new List<PlayerWeaponsSO>(allWeaponSOList);
        List<PlayerWeaponsSO> selected = new();

        for(int i = 0; i < count; i++)
        {
            if (candidates.Count == 0) break;

            int randomIndex = Random.Range(0, candidates.Count);
            selected.Add(candidates[randomIndex]);
            candidates.RemoveAt(randomIndex);
        }

        return selected;
    }

    public void SelectWeapon(PlayerWeaponsSO selectedSO)
    {
        RuntimeWeapon existingWeapon = activeWaepons.Find(w => w.weaponsData == selectedSO);

        if (existingWeapon != null)
        {
            existingWeapon.currentLevel++;
            Debug.Log($"{selectedSO.WeaponName} 레벨업! (lv.{existingWeapon.currentLevel}");
        }
        else
        {
            RuntimeWeapon newWeapon = new RuntimeWeapon(selectedSO);
            activeWaepons.Add(newWeapon);
            SpawnWeaponCountroller(newWeapon);
            Debug.Log($"새 무기 장착 : {selectedSO.WeaponName}");
        }
    }

    private void SpawnWeaponCountroller(RuntimeWeapon weapon)
    {
        GameObject controllerObj = new GameObject($"{weapon.weaponsData.WeaponName}_Controller");
        controllerObj.transform.SetParent(transform);

        BaseWeaponController controller = null;

        switch(weapon.weaponsData.WeaponType)
        {
            case WeaponType.Melee:
                controller = controllerObj.AddComponent<MeleeWeaponController>();
                break;
            case WeaponType.Orbit:

                break;
            case WeaponType.Aura:

                break;
            case WeaponType.Projectile:

                break;
            case WeaponType.Spread:

                break;
            case WeaponType.AreaOfEffect:

                break;
        }

        controller.Init(weapon, playerTransform);
    }
}



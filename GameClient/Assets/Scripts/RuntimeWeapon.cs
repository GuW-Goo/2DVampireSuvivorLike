using UnityEngine;

[System.Serializable]
public class RuntimeWeapon
{
    public PlayerWeaponsSO weaponsData;
    public int currentLevel;

    public RuntimeWeapon(PlayerWeaponsSO data)
    {
        weaponsData = data;
        currentLevel = 0;
    }

    // 레벨 / 버프가 반영된 최종 데미지
    public float GetCalculateDamage(float playerDamageBonus = 1.0f)
    {
        float levelMultiplier = 1.0f + currentLevel * 0.2f;
        return weaponsData.Damage * levelMultiplier * playerDamageBonus;
    }

    // 레벨 / 버프가 반영된 최종 쿨타임
    public float GetCalculatedCooldown(float playerHaste = 0.0f)
    {
        float levelReduction = currentLevel * 0.05f;
        float finalCooldown = weaponsData.Cooldown * (1.0f - levelReduction);
        return Mathf.Max(0.1f, finalCooldown / (1.0f + playerHaste));
    }
}
using UnityEngine;

// 모든 무기가 상속받을 부모 클래스
public abstract class BaseWeaponController : MonoBehaviour
{
    protected RuntimeWeapon runtimeWeapon;
    protected Transform playerTransform;
    protected GameObject spawnedWeaponPrefab;
    private float timer = 0.0f;

    public virtual void Init(RuntimeWeapon weapon, Transform player)
    {
        runtimeWeapon = weapon;
        playerTransform = player;
        transform.SetParent(playerTransform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        if(runtimeWeapon.weaponsData != null && runtimeWeapon.weaponsData.WeaponPrefab != null)
        {
            spawnedWeaponPrefab = Instantiate(runtimeWeapon.weaponsData.WeaponPrefab, transform);
            spawnedWeaponPrefab.transform.localPosition = Vector3.zero;
            spawnedWeaponPrefab.transform.localRotation = Quaternion.identity;

            spawnedWeaponPrefab.SetActive(false);
        }
    }

    protected void Update()
    {
        if (runtimeWeapon == null) return;

        timer += Time.deltaTime;
        if (timer > runtimeWeapon.GetCalculatedCooldown())
        {
            timer = 0.0f;
            Attack();
        }
    }

    // 각 무기가 구현할 공격 로직
    protected abstract void Attack();
}

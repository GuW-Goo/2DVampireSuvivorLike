using UnityEngine;

public class MeleeWeaponController : BaseWeaponController
{
    // 적 몬스터 탐색범위 보정
    [SerializeField] private float extraRange = 5.0f;

    protected override void Attack()
    {
        float serchRadius = runtimeWeapon.weaponsData.Area + extraRange;
        Monster target = MonsterManager.Instance.GetNearestMonster(playerTransform.position, serchRadius);

        if( target != null )
        {

        }
    }
}

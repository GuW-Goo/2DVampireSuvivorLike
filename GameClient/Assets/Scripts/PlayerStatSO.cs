using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatSO", menuName = "Scriptable Objects/PlayerStatSO")]
public class PlayerStatSO : ScriptableObject
{
    [Header("이동 관련 스탯")]
    [SerializeField] private float moveSpeed = 5.0f;

    public float MoveSpeed => moveSpeed;

    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
}

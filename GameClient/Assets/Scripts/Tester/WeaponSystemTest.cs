using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class WeaponSystemTest : MonoBehaviour
{
    private List<PlayerWeaponsSO> currentChoices = new();
    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("===================");
            Debug.Log("레벨업! 무기 선택지를 3개 뽑습니다.");

            currentChoices = WeaponManager.Instance.GetRandomWeaponChoices(3);

            for (int i = 0; i < currentChoices.Count; i++)
            {
                Debug.Log($"선택지 [{i + 1}] : {currentChoices[i].WeaponName} ({currentChoices[i].WeaponType})");
            }
        }

        if (Keyboard.current.digit1Key.wasPressedThisFrame) SelectChoiceByIndex(0);
        if (Keyboard.current.digit2Key.wasPressedThisFrame) SelectChoiceByIndex(1);
        if (Keyboard.current.digit3Key.wasPressedThisFrame) SelectChoiceByIndex(2);
    }

    private void SelectChoiceByIndex(int index)
    {
        if(currentChoices == null || currentChoices.Count <= index)
        {
            Debug.LogWarning("먼저 선택지를 뽑아주세요");
            return;
        }

        PlayerWeaponsSO selectWeapon = currentChoices[index];

        Debug.Log($"[{index + 1}번 \'{selectWeapon.WeaponName}\'을 선택했습니다.");
        WeaponManager.Instance.SelectWeapon(selectWeapon);

        currentChoices.Clear();
    }
}

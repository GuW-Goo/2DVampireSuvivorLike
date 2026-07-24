using UnityEngine;
using UnityEngine.InputSystem;

public class MonsterTrackingTest : MonoBehaviour
{
    [SerializeField] private MonsterSpawner spawner;
    [SerializeField] private MonsterStatSO statSO;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if(Keyboard.current.leftAltKey.isPressed)
        {
            Debug.Log("Try Monster Spawn...");
            spawner.SpawnMonsterFromPool(statSO, Vector2.zero);
            Debug.Log("Monster Spawned");
        }
        
    }


}

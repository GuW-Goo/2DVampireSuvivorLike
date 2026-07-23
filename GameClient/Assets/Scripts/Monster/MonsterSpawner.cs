using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private MonsterPool monsterPool;
    [SerializeField] private MonsterManager monsterManager;

    private float gameTime = 0.0f;

    void Update()
    {
        gameTime += Time.deltaTime;
    }

    public void SpawnMonsterFromPool(MonsterStatSO statSO, Vector2 spawnPosition)
    {
        Monster monster = monsterPool.GetMonster(statSO);

        // 시간에 따른 스탯 증가 비율 계산 (1분마다 체력 20% 증가)
        float hpMultiplier = 1.0f + (gameTime / 60f) * 0.2f;
        float damageMultiplier = 1.0f + (gameTime / 60f) * 0.1f;

        monster.InitMonster(spawnPosition, hpMultiplier, damageMultiplier);

        monsterManager.RegisterMonster(monster);
    }
}

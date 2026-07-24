using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    private Dictionary<MonsterStatSO, Queue<Monster>> poolDic = new();

    public void CreatePool(MonsterStatSO statSO, int count)
    {
        Debug.Log("MonsterPool.CreatePool");
        if(!poolDic.ContainsKey(statSO))
        {
            poolDic.Add(statSO, new Queue<Monster>());
        }

        for(int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(statSO.MonsterPrefab, transform);
            Monster monster = obj.GetComponent<Monster>();

            monster.OnDeath += ReturnMonster;

            obj.SetActive(false);
            poolDic[statSO].Enqueue(monster);
        }
    }

    public Monster GetMonster(MonsterStatSO statSO)
    {
        Debug.Log("MonsterPool.GetMonster()");
        if(!poolDic.ContainsKey(statSO) || poolDic[statSO].Count == 0)
        {
            // 필요시 추가 Instantiate 생성 로직
            CreatePool(statSO, 5);
        }

        Monster monster = poolDic[statSO].Dequeue();
        return monster;
    }

    public void ReturnMonster(Monster monster)
    {
        monster.gameObject.SetActive(false);

        if(poolDic.ContainsKey(monster.StatSO))
        {
            poolDic[monster.StatSO].Enqueue(monster);
        }
    }
}

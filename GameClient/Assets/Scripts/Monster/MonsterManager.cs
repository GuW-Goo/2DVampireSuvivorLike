using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager Instance { get; private set; }

    [SerializeField] private Transform playerTransform;
    private List<Monster> activeMonsters = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RegisterMonster(Monster monster)
    {
        monster.SetTarget(playerTransform);
        activeMonsters.Add(monster);
        monster.OnDeath += UnregisterMonster;
    }

    private void UnregisterMonster(Monster monster)
    {
        monster.OnDeath -= UnregisterMonster;
        activeMonsters.Remove(monster);
    }

    public Monster GetNearestMonster(Vector2 position, float maxRadius = 10.0f)
    {
        Monster nearest = null;
        float minDistanceSqr = maxRadius * maxRadius;

        for (int i = 0; i < activeMonsters.Count; i++)
        {
            if (activeMonsters[i] == null || !activeMonsters[i].gameObject.activeSelf)
                continue;

            Vector2 diff = (Vector2)activeMonsters[i].transform.position - position;
            float sqrDist = diff.sqrMagnitude;

            if (sqrDist < minDistanceSqr)
            {
                minDistanceSqr = sqrDist;
                nearest = activeMonsters[i];
            }
        }

        return nearest;
    }

    void Update()
    {
        float dt = Time.deltaTime;

        for (int i = activeMonsters.Count - 1; i >= 0; i--)
        {
            if (activeMonsters[i] != null && activeMonsters[i].gameObject.activeSelf)
            {
                activeMonsters[i].MoveToTarget(dt);
            }
        }
    }
}

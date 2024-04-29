using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncyclopediaManager : MonoBehaviour
{
    public Dictionary<int, int> monsterDictionary = new Dictionary<int, int>(); // <몬스터 id, 킬 수>

    // 몬스터 가짓수
    private int monsterRaceCount = 10;

    private void Awake()
    {
        for (int i = 1; i <= monsterRaceCount; i++)
        {
            monsterDictionary[i] = 0;
        }
    }

    public void AddKillCount(MonsterData monsterData)
    {
        if (monsterDictionary[monsterData.MonsterId] >= 999) { return; }
        monsterDictionary[monsterData.MonsterId]++;
        Debug.Log($"monsterName : {monsterData.MonsterName}, killcount : {monsterDictionary[monsterData.MonsterId]}");
    }

    public void AddMonsterData(MonsterData monsterData)
    {

    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncyclopediaManager : MonoBehaviour
{
    public Dictionary<int, int> monsterKillDictionary = new Dictionary<int, int>(); // <몬스터 id, 킬 수>

    // 몬스터 가짓수
    private int monsterRaceCount = 10;

    private void Awake()
    {
        for (int i = 1; i <= monsterRaceCount; i++)
        {
            monsterKillDictionary[i] = 0;
        }
    }

    public void AddKillCount(int monsterId)
    {
        if (monsterKillDictionary[monsterId] >= 999) { return; }
        monsterKillDictionary[monsterId]++;
        Debug.Log($"monsterId : {monsterId}, killcount : {monsterKillDictionary[monsterId]}");
    }

}
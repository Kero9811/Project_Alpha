using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncyclopediaManager : MonoBehaviour
{
    public Dictionary<int, int> monsterKillDictionary = new Dictionary<int, int>(); // <���� id, ų ��>

    // ���� ������
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MonsterList", menuName = "Create/MonsterList", order = 3)]
public class MonsterListData : ScriptableObject
{
    [SerializeField] private string[] monsterNames;
    [SerializeField] private MonsterData[] monsterDatas;

    public string[] MonsterNames => monsterNames;
    public MonsterData[] MonsterDatas => monsterDatas;
}
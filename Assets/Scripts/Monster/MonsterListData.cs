using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MonsterList", menuName = "Create/MonsterList", order = 3)]
public class MonsterListData : ScriptableObject
{
    [SerializeField] private string[] monsterNames;

    public string[] MonsterNames => monsterNames;
}
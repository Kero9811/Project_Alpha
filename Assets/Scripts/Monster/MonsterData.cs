using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Monster", menuName = "Create/Monster", order = 0)]
public class MonsterData : ScriptableObject
{
    [SerializeField] private Race race;
    [SerializeField] private string monsterName;
    [SerializeField] private int monsterId;
    [SerializeField] private int maxHp;
    [SerializeField] private int damage;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDelay;
    [SerializeField] private float attackRange;
    [SerializeField] private string desc;
    public Sprite monsterSprite;

    public Race Race => race;
    public string MonsterName => monsterName;
    public int MonsterId => monsterId;
    public int MaxHp => maxHp;
    public int Damage => damage;
    public float MoveSpeed => moveSpeed;
    public float AttackDelay => attackDelay;
    public float AttackRange => attackRange;
    public string Desc => desc;
}
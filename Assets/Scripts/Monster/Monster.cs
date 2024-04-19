using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Race
{
    // 임시 적 종류
    Trap,
    Type_A,
    Type_B,
    Type_C,
    Type_D,
}

public abstract class Monster : MonoBehaviour
{
    public Race race;
    public string monsterName;
    public int maxHp;
    public int curHp;
    public int damage;
    public float moveSpeed;
    public float attackDelay;

    public SO_Monster info;

    private void Awake()
    {
        race = info.Race;
        monsterName = info.MonsterName;
        maxHp = info.MaxHp;
        curHp = maxHp;
        damage = info.Damage;
        moveSpeed = info.MoveSpeed;
        attackDelay = info.AttackDelay;
    }

    public abstract void TakeDamage(int damage, Transform playerTf, bool isDownAttack);
    public abstract void Attack(int damage);
    public abstract void Move();
}
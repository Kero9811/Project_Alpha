using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Race
{
    // �ӽ� �� ����
    Trap,
    Skeleton,
    Type_B,
    Type_C,
    Type_D,
}

public abstract class Monster : MonoBehaviour
{
    protected Race race;
    protected string monsterName;
    protected int monsterId;
    protected int maxHp;
    protected int curHp;
    protected int damage;
    protected float moveSpeed;
    protected float attackDelay;

    public MonsterData info;

    private void Awake()
    {
        race = info.Race;
        monsterName = info.MonsterName;
        monsterId = info.MonsterId;
        maxHp = info.MaxHp;
        curHp = maxHp;
        damage = info.Damage;
        moveSpeed = info.MoveSpeed;
        attackDelay = info.AttackDelay;
    }

    // ����
    public abstract void TakeDamage(int damage);
    // ���� (����)
    public abstract void TakeDamage(int damage, Transform playerTf, bool isDownAttack);
    public abstract void Attack(int damage);
    public abstract void Move();
    public virtual void Die()
    {
        GameManager.Instance.Encyclopedia.AddKillCount(monsterId);
    }
}
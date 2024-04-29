using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Race
{
    // 임시 적 종류
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

    public MonsterData monsterData;

    private void Awake()
    {
        race = monsterData.Race;
        monsterName = monsterData.MonsterName;
        monsterId = monsterData.MonsterId;
        maxHp = monsterData.MaxHp;
        curHp = maxHp;
        damage = monsterData.Damage;
        moveSpeed = monsterData.MoveSpeed;
        attackDelay = monsterData.AttackDelay;
    }

    /// <summary>
    /// 몬스터에게 데미지만 줄 경우
    /// </summary>
    /// <param name="damage">받을 데미지</param>
    public abstract void TakeDamage(int damage);
    /// <summary>
    /// 몬스터 혹은 공격 상호작용 객체에게 데미지와 플레이어 위치, 다운어택 여부 제공하는 함수
    /// </summary>
    /// <param name="damage">받을 데미지</param>
    /// <param name="playerTf">플레이어의 Transform</param>
    /// <param name="isDownAttack">다운어택 여부</param>
    public abstract void TakeDamage(int damage, Transform playerTf, bool isDownAttack);
    public abstract void Attack(int damage);
    public abstract void Move();
    public virtual void Die()
    {
        GameManager.Instance.Encyclopedia.AddKillCount(monsterData);
    }
}
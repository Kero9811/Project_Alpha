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
    /// ���Ϳ��� �������� �� ���
    /// </summary>
    /// <param name="damage">���� ������</param>
    public abstract void TakeDamage(int damage);
    /// <summary>
    /// ���� Ȥ�� ���� ��ȣ�ۿ� ��ü���� �������� �÷��̾� ��ġ, �ٿ���� ���� �����ϴ� �Լ�
    /// </summary>
    /// <param name="damage">���� ������</param>
    /// <param name="playerTf">�÷��̾��� Transform</param>
    /// <param name="isDownAttack">�ٿ���� ����</param>
    public abstract void TakeDamage(int damage, Transform playerTf, bool isDownAttack);
    public abstract void Attack(int damage);
    public abstract void Move();
    public virtual void Die()
    {
        GameManager.Instance.Encyclopedia.AddKillCount(monsterData);
    }
}
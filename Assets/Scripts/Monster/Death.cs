using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DeathState
{
    Idle,
    Walk,
    Attack,
    Cast,
    Dead
}

public class Death : Monster
{
    [HideInInspector] public Animator anim;
    Rigidbody2D rb;
    Player player;
    DeathState state;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Attack()
    {
    }

    private void MeleeAttack()
    {

    }

    private void ShortCast()
    {

    }

    private void LongCast()
    {

    }

    public override void Move()
    {
        if (player != null)
        {

        }
    }

    public override void TakeDamage(int damage)
    {
        if (state == DeathState.Dead) { return; }

        curHp -= damage;

        if (curHp <= 0)
        {
            curHp = 0;
            Die();
        }
    }

    public override void Die()
    {
        base.Die();
        anim.SetTrigger("Dead");
    }

    #region 미사용 메서드
    public override void TakeDamage(int damage, Transform playerTf, bool isDownAttack)
    {
    }
    #endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Monster
{
    public override void Attack(int damage)
    {
    }


    public override void Move()
    {
    }

    public override void TakeDamage(int damage)
    {
        curHp -= damage;

        if (curHp <= 0)
        {
            curHp = 0;
            Die();
        }
    }

    public override void TakeDamage(int damage, Transform playerTf, bool isDownAttack)
    {
    }

    public override void Die()
    {
        base.Die();
    }

}
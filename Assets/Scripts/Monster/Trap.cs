using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Monster
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            player.TakeDamage(damage, transform);
        }
    }

    public override void TakeDamage(int damage, Transform playerTf, bool isDownAttack)
    {
        if (isDownAttack)
        {
            playerTf.GetComponent<PlayerMove>().DownAttackJump();
        }
    }

    #region 미사용 메서드
    public override void Attack()
    {
    }

    public override void Move()
    {
    }

    public override void TakeDamage(int damage)
    {
    }
    #endregion
}
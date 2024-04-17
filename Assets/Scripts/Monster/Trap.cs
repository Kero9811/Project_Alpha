using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Monster
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            player.TakeDamage(damage);
        }
    }

    public override void TakeDamage(int damage)
    {
        // 데미지는 받지 않고 소리가 나게 하려면 추가
    }

    #region 사용하지 않는 메서드
    public override void Attack(int damage)
    {
    }

    public override void Move()
    {
    }
    #endregion
}
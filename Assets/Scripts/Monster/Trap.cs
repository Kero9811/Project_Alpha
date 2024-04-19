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

    public override void TakeDamage(int damage, Transform playerTf, bool isDownAttack)
    {
        // �������� ���� �ʰ� �Ҹ��� ���� �Ϸ��� �߰�
        // bool is true�� �÷��̾��� �Լ� ���� (���� �̵�)
        if (isDownAttack)
        {
            playerTf.GetComponent<PlayerMove>().DownAttackJump();
        }
    }

    #region ������� �ʴ� �޼���
    public override void Attack(int damage)
    {
    }

    public override void Move()
    {
    }
    #endregion
}
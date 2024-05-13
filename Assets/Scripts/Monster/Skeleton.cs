using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    Move,
    Attack,
    Block,
    Dead
}

public class Skeleton : Monster
{
    Rigidbody2D rb;
    Player player;
    Animator anim;
    State state;

    private bool playerIn;
    private bool isAction;
    private bool blocked;

    private float blockTime = 4f;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerIn = true;
            player = collision.GetComponent<Player>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerIn = false;
            player = null;
        }
    }

    public override void Attack(int damage)
    {
        if (blocked)
        {
            StopCoroutine(BlockCoroutine());
            // 2단 공격
            anim.SetTrigger("Attack_2");
            blocked = false;
        }
        else
        {
            // 1단 공격
            anim.SetTrigger("Attack_1");
        }
    }

    private IEnumerator BlockCoroutine()
    {
        while (isAction)
        {
            // 막기 설정
            anim.SetBool("Shield", true);
            yield return new WaitForSeconds(blockTime);

            anim.SetBool("Shield", false);
            Attack(damage);
        }
    }


    public override void Move()
    {
        if (playerIn)
        {
            if (attackRange >= Vector3.Distance(transform.position, player.transform.position))
            {
                if (isAction) { return; }
                isAction = true;
                rb.velocity = Vector3.zero;
                anim.SetInteger("MoveSpeed", 0);
                _ = StartCoroutine(BlockCoroutine());
                return;
            }
            else
            {
                isAction = false;
                anim.SetBool("Shield", false);
            }

            float x = Mathf.Abs(transform.localScale.x);
            float y = Mathf.Abs(transform.localScale.y);
            int setDir = transform.position.x <= player.transform.position.x ? 1 : -1;

            rb.velocity = new Vector3(setDir * moveSpeed, 0, 0);
            transform.localScale = new Vector3(setDir * x, y, 1);
            anim.SetInteger("MoveSpeed", (int)rb.velocity.x);
            state = State.Move;
        }
    }

    public override void TakeDamage(int damage)
    {
        curHp -= damage;

        if (curHp <= 0)
        {
            curHp = 0;
            Die();
            return;
        }
    }

    public override void TakeDamage(int damage, Transform playerTf, bool isDownAttack)
    {
        curHp -= damage;
        Debug.Log("다운 어택에 의한 데미지!");

        if (curHp <= 0)
        {
            curHp = 0;
            Die();
        }
    }

    public override void Die()
    {
        base.Die();
    }

}
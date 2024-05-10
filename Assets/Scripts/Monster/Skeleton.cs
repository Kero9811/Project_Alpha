using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Monster
{
    Rigidbody2D rb;
    Player player;
    Animator anim;

    private bool playerIn;
    private bool isAction;
    private bool blocked;

    private float blockTime = 2f;

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
            blocked = false;
        }
        else
        {
            // 1단 공격
        }
    }

    private IEnumerator BlockCoroutine()
    {
        // 막기 설정
        yield return new WaitForSeconds(blockTime);

        Attack(damage);
    }


    public override void Move()
    {
        if (playerIn)
        {
            if (attackRange >= Vector3.Distance(transform.position, player.transform.position))
            {
                if (isAction) { return; }
                isAction = true;
                _ = StartCoroutine(BlockCoroutine());
                return;
            }
            else
            {
                isAction = false;
            }

            Vector3 dir = (player.transform.position - transform.position).normalized;
            dir.y = 0;
            rb.MovePosition(rb.transform.position + dir * moveSpeed * Time.fixedDeltaTime);
            Debug.Log(rb.velocity.magnitude);
            anim.SetFloat("MoveSpeed", dir.magnitude * moveSpeed);
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
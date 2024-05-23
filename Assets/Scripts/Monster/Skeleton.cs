using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkulState
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
    SkulState state;

    private bool playerIn;
    private bool isAction;
    private bool isBlocking; // 막는 중 체크
    private bool blocked; // 막기 성공

    private float blockTime = 4f;

    Coroutine blockCoroutine;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if (state == SkulState.Dead) { return; }

        Move();
        FlipRenderer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerIn = true;
            player = collision.GetComponent<Player>();
            state = SkulState.Move;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerIn = false;
            player = null;
            state = SkulState.Idle;
        }
    }

    public override void Attack()
    {
        state = SkulState.Attack;

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
            isBlocking = true;
            yield return new WaitForSeconds(blockTime);

            anim.SetBool("Shield", false);
            isBlocking = false;
            Attack();
        }
    }


    public override void Move()
    {
        if (playerIn)
        {
            if (attackRange >= Mathf.Abs(transform.position.x - player.transform.position.x))
            {
                if (isAction) { return; }
                isAction = true;
                rb.velocity = Vector3.zero;
                anim.SetInteger("MoveSpeed", 0);
                state = SkulState.Block;
                if (blockCoroutine != null) { return; }
                blockCoroutine = StartCoroutine(BlockCoroutine());
                return;
            }
            else
            {
                isAction = false;
                anim.SetBool("Shield", false);
                if (state != SkulState.Attack)
                {
                    state = SkulState.Move;
                }
                if (blockCoroutine != null)
                {
                    StopCoroutine(blockCoroutine);
                    blockCoroutine = null;
                }
            }

            if (state != SkulState.Move) { return; }

            int setDir = transform.position.x <= player.transform.position.x ? 1 : -1;

            rb.velocity = new Vector3(setDir * moveSpeed, rb.velocity.y, 0);
            anim.SetInteger("MoveSpeed", (int)rb.velocity.x);
            state = SkulState.Move;
        }
    }

    private void FlipRenderer()
    {
        if (player == null || state == SkulState.Attack) { return; }

        float x = Mathf.Abs(transform.localScale.x);
        float y = Mathf.Abs(transform.localScale.y);

        int setDir = transform.position.x <= player.transform.position.x ? 1 : -1;

        transform.localScale = new Vector3(setDir * x, y, 1);
    }

    public override void TakeDamage(int damage)
    {
        if (state == SkulState.Dead) return;

        if (isBlocking)
        {
            blocked = true;
            isBlocking = false;
            StopCoroutine(blockCoroutine);
            blockCoroutine = null;
            Invoke("Attack", .5f);
            return;
        }

        if (state == SkulState.Block) { print("Block"); return; }

        curHp -= damage;

        if (curHp <= 0)
        {
            curHp = 0;
            Die();
        }
    }

    public override void TakeDamage(int damage, Transform playerTf, bool isDownAttack)
    {
        if (state == SkulState.Dead) return;

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

        state = SkulState.Dead;
        anim.SetTrigger("Dead");
        Invoke("CleanMonster", 2f);
    }

    public void DeactivateisAction()
    {
        isAction = false;
        state = SkulState.Move;
    }
}
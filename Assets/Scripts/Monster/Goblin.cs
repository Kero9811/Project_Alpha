using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoblinState
{
    Idle,
    Move,
    Hit,
    Attack,
    Dead
}

public class Goblin : Monster
{
    Rigidbody2D rb;
    Player player;
    Animator anim;
    GoblinState state;

    private bool playerIn;
    private bool isInRange;
    private bool isCD;
    private bool isActive;
    Coroutine attackCoroutine;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        anim  = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if (state == GoblinState.Dead) { return; }

        Move();
        FlipRenderer();
        CheckInRange();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerIn = true;
            player = collision.GetComponent<Player>();
            state = GoblinState.Move;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerIn = false;
            player = null;
            state = GoblinState.Idle;
        }
    }

    public override void Attack()
    {
        isActive = true;
        attackCoroutine = StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        while(isActive)
        {
            isCD = true;
            state = GoblinState.Attack;
            anim.SetTrigger("Attack_1");
            yield return new WaitForSeconds(attackDelay);
            isCD = false;
        }
    }

    public override void Move()
    {
        if (playerIn)
        {
            if (isInRange && attackCoroutine == null && !isCD)
            {
                rb.velocity = Vector3.zero;
                anim.SetInteger("MoveSpeed", 0);
                Attack();
                return;
            }
            else if (!isInRange && state == GoblinState.Idle)
            {
                state = GoblinState.Move;
                //if (attackCoroutine != null) { StopCoroutine(attackCoroutine); }
                isActive = false;
                attackCoroutine = null;
            }

            if (isCD) { return; }

            int setDir = transform.position.x <= player.transform.position.x ? 1 : -1;

            rb.velocity = new Vector3(setDir * moveSpeed, rb.velocity.y, 0);
            anim.SetInteger("MoveSpeed", (int)rb.velocity.x);
            state = GoblinState.Move;
        }
    }

    private void FlipRenderer()
    {
        if (player == null || state == GoblinState.Attack) { return; }

        float x = Mathf.Abs(transform.localScale.x);
        float y = Mathf.Abs(transform.localScale.y);

        int setDir = transform.position.x <= player.transform.position.x ? 1 : -1;

        transform.localScale = new Vector3(setDir * x, y, 1);
    }

    private void CheckInRange()
    {
        if (player != null)
        {
            isInRange = attackRange >= Mathf.Abs(transform.position.x - player.transform.position.x);
        }
    }

    public override void TakeDamage(int damage)
    {
        if (state == GoblinState.Dead) { return; }

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

        anim.SetTrigger("Dead");
        Invoke("CleanMonster", 2f);
    }

    public void SetGoblinState()
    {
        state = GoblinState.Idle;
    }
}
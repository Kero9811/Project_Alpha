using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Damage Stat")]

    [SerializeField] private int damage;
    [SerializeField] private int magicDamage; // 추후 추가

    private float delay = .3f;
    private float attackCD = 0;


    [Space(20)]

    private Transform attackTf;
    private Transform downAttackTf;
    private Vector2 attackSize = new Vector2(1, 1);
    [SerializeField] private GameObject attackEffect;

    Player player;
    Animator anim;

    private void Awake()
    {
        attackTf = transform.Find("AttackPoint").transform;
        downAttackTf = transform.Find("Foot").transform;
        player = GetComponent<Player>();
        anim = transform.Find("Renderer").GetComponent<Animator>();
    }

    private void Update()
    {
        attackCD -= Time.deltaTime;
    }

    private void Attack(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.UI.isOpen) { return; }

        if (player.CurState == PlayerState.Dead ||
            player.CurState == PlayerState.ChargeHeal ||
            player.CurState == PlayerState.Dash ||
            player.CurState == PlayerState.LookAt ||
            player.CurState == PlayerState.GroundSmash) { return; }

        // 어택 판정 생성
        if (/*context.started && attackCD <= 0*/ context.duration < .2f && context.canceled && attackCD <= 0)
        {
            attackCD = delay;
            //if (player.CurState == PlayerState.Idle)
            //{
            //    anim.SetTrigger("Attack");
            //}
            Debug.Log("Attack");
            Collider2D[] attackCols = Physics2D.OverlapBoxAll(attackTf.position, attackSize, 0);
            LeanPool.Spawn(attackEffect, attackTf, false);

            for (int i = 0; i < attackCols.Length; i++)
            {
                if (attackCols[i].TryGetComponent(out Monster monster))
                {
                    Debug.Log(monster.name);
                    //monster.TakeDamage(damage, transform, false);
                    monster.TakeDamage(damage);
                    player.GetMp(5);
                    GameManager.Instance.UI.m_Handler.OnChangeMp();
                }
            }
        }
    }

    private void DownAttack(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.UI.isOpen) { return; }

        if (player.CurState == PlayerState.Dead ||
            player.CurState == PlayerState.ChargeHeal ||
            player.CurState == PlayerState.Dash ||
            player.CurState == PlayerState.LookAt ||
            player.CurState == PlayerState.GroundSmash) { return; }

        // 어택 판정 생성
        if (context.started && attackCD <= 0)
        {
            attackCD = delay;
            //if (player.CurState == PlayerState.Idle)
            //{
            //    anim.SetTrigger("Attack");
            //}
            Debug.Log("Down Attack");
            Collider2D[] attackCols = Physics2D.OverlapBoxAll(downAttackTf.position, attackSize, 0);

            for (int i = 0; i < attackCols.Length; i++)
            {
                if (attackCols[i].TryGetComponent(out Monster monster))
                {
                    Debug.Log("Down Attack");
                    Debug.Log(monster.name);
                    monster.TakeDamage(damage, transform, true);
                    player.GetMp(5);
                    GameManager.Instance.UI.m_Handler.OnChangeMp();
                }
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(downAttackTf.position, attackSize);
    //}
}

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

    public int Damage => damage;
    public int MagicDamage => magicDamage;

    private float delay = .3f;
    private float attackCD = 0;


    [Space(20)]

    private Transform attackTf;
    private Transform upAttackTf;
    private Transform downAttackTf;
    private Vector2 attackSize = new Vector2(1, 1);

    [SerializeField] private GameObject attackEffect;
    [SerializeField] private GameObject upAttackEffect;

    Player player;
    Animator anim;

    private void Awake()
    {
        attackTf = transform.Find("AttackPoint").transform;
        upAttackTf = transform.Find("Head").transform;
        downAttackTf = transform.Find("Foot").transform;
        player = GetComponent<Player>();
        anim = transform.Find("Renderer").GetComponent<Animator>();
    }

    private void Update()
    {
        attackCD -= Time.deltaTime;
    }

    public void SetPlayerDamage(PlayerStatus playerStat)
    {
        damage = playerStat.damage;
        magicDamage = playerStat.magicDamage;
    }

    private void Attack(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.UI.isInvenOpen) { return; }

        if (player.CurState == PlayerState.Dead ||
            player.CurState == PlayerState.ChargeHeal ||
            player.CurState == PlayerState.Dash ||
            player.CurState == PlayerState.LookAt ||
            player.CurState == PlayerState.GroundSmash) { return; }

        // 어택 판정 생성
        if (context.duration < .2f && context.canceled && attackCD <= 0)
        {
            attackCD = delay;
            //if (player.CurState == PlayerState.Idle)
            //{
            //    anim.SetTrigger("Attack");
            //}
            Collider2D[] attackCols = Physics2D.OverlapBoxAll(attackTf.position, attackSize, 0);
            LeanPool.Spawn(attackEffect, attackTf, false);

            for (int i = 0; i < attackCols.Length; i++)
            {
                if (attackCols[i].TryGetComponent(out Monster monster))
                {
                    Debug.Log(monster.name);
                    monster.TakeDamage(damage);
                    player.GetMp(5);
                    GameManager.Instance.UI.m_Handler.OnChangeMp();
                }
            }
        }
    }

    private void UpAttack(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.UI.isInvenOpen) { return; }

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
            Collider2D[] attackCols = Physics2D.OverlapBoxAll(upAttackTf.position, attackSize, 0);
            GameObject effect = LeanPool.Spawn(upAttackEffect, upAttackTf, false);
            effect.transform.localScale = new Vector3(.2f, .2f, .2f);

            for (int i = 0; i < attackCols.Length; i++)
            {
                if (attackCols[i].TryGetComponent(out Monster monster))
                {
                    Debug.Log(monster.name);
                    monster.TakeDamage(damage);
                    player.GetMp(5);
                    GameManager.Instance.UI.m_Handler.OnChangeMp();
                }
            }
        }
    }

    private void DownAttack(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.UI.isInvenOpen) { return; }

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
            Collider2D[] attackCols = Physics2D.OverlapBoxAll(downAttackTf.position, attackSize, 0);
            GameObject effect = LeanPool.Spawn(upAttackEffect, downAttackTf, false);
            effect.transform.localScale = new Vector3(.2f, -.2f, .2f);

            for (int i = 0; i < attackCols.Length; i++)
            {
                if (attackCols[i].TryGetComponent(out Monster monster))
                {
                    Debug.Log(monster.name);
                    monster.TakeDamage(damage, transform, true);
                    player.GetMp(5);
                    GameManager.Instance.UI.m_Handler.OnChangeMp();
                }
            }
        }
    }

    public void MeleeDmgUp(int value)
    {
        damage += value;

        GameManager.Instance.Data.SavePlayerData(player);
    }

    public void MagicDmgUp(int value)
    {
        magicDamage += value;

        GameManager.Instance.Data.SavePlayerData(player);
    }
}

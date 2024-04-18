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
    private Vector2 attackSize = new Vector2(1, 1);

    Player player;
    Animator anim;

    private void Awake()
    {
        attackTf = transform.Find("AttackPoint").transform;
        player = GetComponent<Player>();
        anim = transform.Find("Renderer").GetComponent<Animator>();
    }

    private void Update()
    {
        attackCD -= Time.deltaTime;
    }

    private void Attack(InputAction.CallbackContext context)
    {
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

            Collider2D[] attackCols = Physics2D.OverlapBoxAll(attackTf.position, attackSize, 0);

            for (int i = 0;  i < attackCols.Length; i++)
            {
                if (attackCols[i].TryGetComponent(out Monster monster))
                {
                    Debug.Log(monster.name);
                    // 레이어 비교 or 태그 비교 해서 함정과 몬스터를 구분 => TakeDamage 함수 호출 구분위해
                    monster.TakeDamage(damage);
                    player.GetMp(5);
                    UIManager.Instance.m_Handler.OnChangeMp();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(attackTf.position, attackSize);
    }
}

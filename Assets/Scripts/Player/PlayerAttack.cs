using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Damage Stat")]

    [SerializeField] private int damage;
    [SerializeField] private int magicDamage; // ���� �߰�

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

        // ���� ���� ����
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
                    // ���̾� �� or �±� �� �ؼ� ������ ���͸� ���� => TakeDamage �Լ� ȣ�� ��������
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

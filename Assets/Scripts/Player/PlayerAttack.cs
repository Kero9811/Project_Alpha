using System.Collections;
using System.Collections.Generic;
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

    public GameObject prefab;
    private Transform attackPos;

    Player player;
    Animator anim;

    private void Awake()
    {
        attackPos = transform.Find("Front").transform;
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
            print("Attack");
            attackCD = delay;
            if (player.CurState == PlayerState.Idle)
            {
                anim.SetTrigger("Attack");
            }
            
            if (true /*���Ͱ� ������*/)
            {
                // ������ ���� ��������
                player.GetMp(5);
                UIManager.Instance.m_Handler.OnChangeMp();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
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

        // 어택 판정 생성
        if (context.started && attackCD <= 0)
        {
            print("Attack");
            attackCD = delay;
            if (player.CurState == PlayerState.Idle)
            {
                anim.SetTrigger("Attack");
            }
            
            if (true /*몬스터가 맞으면*/)
            {
                // 마나가 조금 차오른다
                player.GetMp(5);
                UIManager.Instance.m_Handler.OnChangeMp();
            }
        }
    }
}

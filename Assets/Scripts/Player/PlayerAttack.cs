using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Damage Stat")]

    [SerializeField] private int damage;
    [SerializeField] private int magicDamage; // 추후 추가

    private float delay = .5f;
    private float attackCD = 0;


    [Space(20)]

    public GameObject prefab;
    private Transform attackPos;

    Player player;

    private void Awake()
    {
        attackPos = transform.Find("Right").transform;
        player = GetComponent<Player>();
    }

    private void Update()
    {
        attackCD -= Time.deltaTime;
    }

    private void Attack(InputAction.CallbackContext context)
    {
        // 어택 판정 생성
        if (context.started && attackCD <= 0)
        {
            player.SetCurState(PlayerState.ATTACK);
            print("Attack");
            attackCD = delay;
        }
    }
}

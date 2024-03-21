using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Damage Stat")]

    [SerializeField] private int damage;
    [SerializeField] private int magicDamage; // 추후 추가

    [Space(20)]

    public GameObject prefab;
    private Transform attackPos;

    private void Awake()
    {
        attackPos = transform.Find("Right").transform;
    }

    void OnAttack(InputValue value)
    {
        // 어택 판정 생성
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum PlayerState
{
    Idle,
    Move,
    Dash,
    Jump,
    WallSlide,
    WallJump,
    Attack,
    Hit,
    GroundSmash,
    ChargeHeal,
    Dead
}

public class Player : MonoBehaviour
{
    #region 플레이어 스탯 및 상태
    private int maxHp = 100;
    private int curHp;
    private int maxMp = 100;
    private int curMp;
    private int curGold;
    private int maxGold;
    private PlayerState curState;
    public int CurHp => curHp;
    public int MaxHp => maxHp;
    public int CurMp => curMp;
    public int MaxMp => maxMp;
    public int CurGold => curGold;
    public int MaxGold => maxGold;
    public PlayerState CurState => curState;
    #endregion

    #region 플레이어의 기능 스크립트 참조
    private PlayerMove p_Move;
    private PlayerAttack p_Attack;
    private PlayerSkill p_Skill;
    public PlayerMove P_Move => p_Move;
    public PlayerAttack P_Attack => p_Attack;
    public PlayerSkill P_Skill => p_Skill;
    #endregion

    Animator anim;

    private void Awake()
    {
        p_Move = GetComponent<PlayerMove>();
        p_Attack = GetComponent<PlayerAttack>();
        p_Skill = GetComponent<PlayerSkill>();

        anim = transform.Find("Renderer").GetComponent<Animator>();

        // 추후 세이브 데이터에서 가져오는걸로 변경
        curHp = maxHp;
        curMp = maxMp;
    }

    public void TakeDamage(int damage)
    {
        curHp -= damage;

        if (curHp <= 0)
        {
            curHp = 0;
            Die();
        }
    }

    public bool UseMp(int cost)
    {
        if (curMp < cost)
        {
            Debug.Log("마나 부족");
            return false;
        }

        curMp -= cost;
        UIManager.Instance.m_Handler.OnChangeMp();
        return true;
    }

    public void Heal(int value)
    {
        curHp += value;

        if (curHp >= maxHp)
        {
            curHp = maxHp;
        }
    }

    private void Die()
    {
        anim.SetTrigger("Dead");
    }

    public void SetCurState(PlayerState state)
    {
        curState = state;
    }

}

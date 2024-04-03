using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum PlayerState
{
    IDLE,
    MOVE,
    DASH,
    JUMP,
    SWIMMING,
    ATTACK,
    HIT,
    GROUNDSMASH,
    CHARGEHEAL,
    DEAD
}

public class Player : MonoBehaviour
{
    #region �÷��̾� ���� �� ����
    private int curHp;
    private int maxHp;
    private int curMp;
    private int maxMp;
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

    #region �÷��̾��� ��� ��ũ��Ʈ ����
    private PlayerMove p_Move;
    private PlayerAttack p_Attack;
    private PlayerSkill p_Skill;
    public PlayerMove P_Move => p_Move;
    public PlayerAttack P_Attack => p_Attack;
    public PlayerSkill P_Skill => p_Skill;
    #endregion

    private void Awake()
    {
        p_Move = GetComponent<PlayerMove>();
        p_Attack = GetComponent<PlayerAttack>();
        p_Skill = GetComponent<PlayerSkill>();
    }

    public void SetCurState(PlayerState state)
    {
        curState = state;
    }

}

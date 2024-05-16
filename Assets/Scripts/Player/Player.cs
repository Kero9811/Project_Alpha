using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using System.Threading.Tasks;
using System.Threading;

[Serializable]
public enum PlayerState
{
    Idle,
    Move,
    Dash,
    Jump, // 여기까지 순서 고정 (애니메이션 때문에)
    LookAt,
    WallSlide,
    WallJump,
    GroundSmash,
    ChargeHeal,
    Stun,
    Dead
}

public class Player : MonoBehaviour
{
    #region 플레이어 스탯 및 상태
    private int maxHp; // 최종 수치 : 10HP
    private int curHp;
    private int maxMp;
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
    SpriteRenderer render;
    Rigidbody2D rb;

    Coroutine blinkCoroutine;
    private float blinkDuration = 2f;
    private float blinkInterval = 0.2f;

    #region 이펙트 프리팹
    [SerializeField] private GameObject healEffectPrefab;
    #endregion

    private void Awake()
    {
        p_Move = GetComponent<PlayerMove>();
        p_Attack = GetComponent<PlayerAttack>();
        p_Skill = GetComponent<PlayerSkill>();

        anim = transform.Find("Renderer").GetComponent<Animator>();
        render = transform.Find("Renderer").GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private async void Start()
    {
        await Task.Run(() =>
        {
            while(GameManager.Instance.Data == null)
            {
                Thread.Sleep(10);
            }
        });

        GameManager.Instance.Data.LoadPlayerData(this);
        GameManager.Instance.UI.UpdateGameUI();
        Debug.Log("플레이어 데이터 불러오기 완료");
    }

    public void SetPlayerStat(PlayerStatus playerStat)
    {
        maxHp = playerStat.maxHp;
        curHp = playerStat.curHp;
        maxMp = playerStat.maxMp;
        curMp = playerStat.curMp;
        curGold = playerStat.curGold;
        maxGold = playerStat.maxGold;
    }

    public void TakeDamage(int damage, Transform enemyTf)
    {
        if (curState == PlayerState.Dead) { return; }

        curHp -= damage;
        Knockback(enemyTf);
        Time.timeScale = .4f;
        Invoke("RestoreTimeScale", .2f);
        GameManager.Instance.UI.h_Handler.OnChangeHp();
        if (curHp <= 0)
        {
            curHp = 0;
            Die();
            return;
        }
        OnDamaged();
    }

    private void RestoreTimeScale()
    {
        Time.timeScale = 1f;
    }

    private void Knockback(Transform enemyTf)
    {
        curState = PlayerState.Stun;
        rb.velocity = Vector3.zero;
        Vector2 dir = (transform.position - enemyTf.position).normalized;
        rb.AddForce((Vector2.up + dir) * 2f, ForceMode2D.Impulse);
    }

    private void OnDamaged()
    {
        gameObject.layer = 8;
        blinkCoroutine = StartCoroutine(OnDamagedCoroutine());
    }

    IEnumerator OnDamagedCoroutine()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }

        float startTime = Time.time;
        while (Time.time - startTime < blinkDuration)
        {
            float alpha = Mathf.PingPong(Time.time / blinkInterval, 1f);
            render.color = new Color(render.color.r, render.color.g, render.color.b, alpha);
            yield return null;
        }
        gameObject.layer = 7;
        render.color = new Color(render.color.r, render.color.g, render.color.b, 1f);
        yield return null;
    }

    public bool UseMp(int cost)
    {
        if (curMp < cost)
        {
            Debug.Log("마나 부족");
            return false;
        }

        curMp -= cost;
        GameManager.Instance.UI.m_Handler.OnChangeMp();
        return true;
    }

    public void Heal(int value)
    {
        curHp += value;

        if (curHp > maxHp)
        {
            curHp = maxHp;
            return;
        }

        LeanPool.Spawn(healEffectPrefab, P_Move.FootTf);
        GameManager.Instance.UI.h_Handler.OnChangeHp();
    }

    public void GetMp(int value)
    {
        curMp += value;

        if (curMp > maxMp)
        {
            curMp = maxMp;
            return;
        }

        GameManager.Instance.UI.m_Handler.OnChangeMp();
    }

    public void GetMaxHp(int maxValue)
    {
        maxHp += maxValue;

        if (maxHp > 10) { maxHp = 10; return; }

        curHp += maxValue;

        GameManager.Instance.UI.h_Handler.OnChangeMaxHp();
        GameManager.Instance.UI.h_Handler.OnChangeHp();

        GameManager.Instance.Data.SavePlayerData(this);
    }

    public void GetMaxMp(int maxValue)
    {
        maxMp += maxValue;

        if(maxMp > 200) {  maxMp = 200; return; }

        curMp += maxValue;
        GameManager.Instance.UI.m_Handler.OnChangeMaxMp();
        GameManager.Instance.UI.m_Handler.OnChangeMp();
    }

    public void GetGold(int value)
    {
        curGold += value;

        if (curGold > 9999) { curGold = 9999; return; }

        GameManager.Instance.UI.moneyHandler.OnChangeMoney();
    }

    public void UseGold(int value)
    {
        curGold -= value;

        GameManager.Instance.UI.moneyHandler.OnChangeMoney();
    }

    private void Die()
    {
        anim.SetTrigger("Dead");
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }
        render.color = new Color(1, 1, 1, 1);
        SetCurState(PlayerState.Dead);
    }

    public void SetCurState(PlayerState state)
    {
        curState = state;
    }

}

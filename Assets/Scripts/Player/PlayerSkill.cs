using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using Lean.Pool;

public class PlayerSkill : MonoBehaviour
{
    #region 그라운드 스매쉬
    private float fallSpeed = 14f;
    private bool canGrandSmash = true;
    private float smashCD = 1f;
    private bool blockLoop = false; // 스매싱 무한 루프 방지
    #endregion

    #region 차지힐
    private bool canChargerHeal = false;
    Coroutine chargeHealCoroutine = null;
    private int healMp = 30;
    #endregion

    #region 각종 변수
    Rigidbody2D rb;
    Animator anim;
    PlayerMove playerMove;
    Player player;
    CinemachineImpulseSource c_Source;
    #endregion

    [SerializeField] private GameObject healChargeEffectPrefab;

    private GameObject chargeObj;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = transform.Find("Renderer").GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
        player = GetComponent<Player>();
        c_Source = GetComponent<CinemachineImpulseSource>();
    }

    private void GroundSmash(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.UI.isInvenOpen) { return; }

        if (player.CurState == PlayerState.Dash || 
            player.CurState == PlayerState.Dead) { return; }

        if (!playerMove.isGround && canGrandSmash && context.started)
        {
            StartCoroutine(Smash());
        }
    }

    private void SpellOrHeal(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.UI.isInvenOpen) { return; }

        bool canUse = true;

        if (player.CurState == PlayerState.GroundSmash ||
            player.CurState == PlayerState.Dash ||
            player.CurState == PlayerState.LookAt ||
            player.CurState == PlayerState.Dead) { return; }

        // 공중에서 마법은 가능하고 힐 차징은 안되도록
        if (player.CurState == PlayerState.Jump) { canUse = false; }

        if (context.duration < .5f && context.canceled)
        {
            anim.SetTrigger("Shot");
        }
        else if (context.duration > 1f && (context.performed || context.canceled) && canUse)
        {
            if (player.CurMp >= healMp && player.CurHp != player.MaxHp)
            {
                canChargerHeal = true;
            }
            else
            {
                canChargerHeal = false;
            }

            FocusHeal();
        }
    }

    private void FocusHeal()
    {
        if (!canChargerHeal)
        {
            LeanPool.Despawn(chargeObj);
            return;
        }

        if (chargeHealCoroutine == null)
        {
            chargeObj = LeanPool.Spawn(healChargeEffectPrefab, player.P_Move.FootTf);
            chargeHealCoroutine = StartCoroutine(ChargeHeal());
        }
        else
        {
            anim.SetBool("isChargeHeal", false);
            LeanPool.Despawn(chargeObj);
            player.SetCurState(PlayerState.Idle);
            StopCoroutine(chargeHealCoroutine);
            chargeHealCoroutine = null;
        }
    }

    #region 그라운드 스매쉬 코루틴
    IEnumerator Smash()
    {
        float curTime = 0f;
        canGrandSmash = false;
        player.SetCurState(PlayerState.GroundSmash);
        float originGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        playerMove.StopMove();
        anim.SetTrigger("GroundSlam");
        while (!(playerMove.isGround || blockLoop))
        {
            rb.velocity = new Vector2(0f, Vector2.down.y * fallSpeed);
            curTime += Time.deltaTime;
            if (curTime > 3f)
            {
                blockLoop = true;
            }
            yield return null;
        }
        rb.gravityScale = originGravity;
        c_Source.GenerateImpulseWithForce(5f);
        anim.speed = 1f;
        yield return new WaitForSeconds(smashCD);
        canGrandSmash = true;
        blockLoop = false;
    }
    #endregion

    #region 차지힐 코루틴
    IEnumerator ChargeHeal()
    {
        float curTime = 0f;
        player.SetCurState(PlayerState.ChargeHeal);

        anim.SetBool("isChargeHeal", true);

        while (true)
        {
            curTime += Time.deltaTime;

            if (curTime > 1f)
            {
                if (player.CurHp == player.MaxHp || player.CurMp < healMp)
                {
                    anim.SetBool("isChargeHeal", false);
                    player.SetCurState(PlayerState.Idle);
                    StopCoroutine(chargeHealCoroutine);
                    chargeHealCoroutine = null;
                    break;
                }
                else
                {
                    curTime = .2f;
                }

                player.UseMp(healMp);
                player.Heal(1);
                //LeanPool.Spawn(healEffectPrefab, player.P_Move.FootTf);
            }
            yield return null;
        }
    }
    #endregion
}

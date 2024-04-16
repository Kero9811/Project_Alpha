using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerSkill : MonoBehaviour
{
    #region �׶��� ���Ž�
    private float fallSpeed = 15f;
    private bool canGrandSmash = true;
    private float smashCD = 1f;
    private bool blockLoop = false; // ���Ž� ���� ���� ����
    #endregion

    #region ������
    private bool canChargerHeal = true;
    Coroutine chargeHealCoroutine = null;
    #endregion

    Rigidbody2D rb;
    Animator anim;
    PlayerMove playerMove;
    Player player;
    CinemachineImpulseSource c_Source;

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
        if (player.CurState == PlayerState.Dash || 
            player.CurState == PlayerState.Dead) { return; }

        // TODO: �� ��ų�� �ر��Ͽ��ٸ� ��밡��
        if (!playerMove.isGround && canGrandSmash && context.started)
        {
            StartCoroutine(Smash());
        }
    }

    private void SpellOrHeal(InputAction.CallbackContext context)
    {
        bool canUse = true;

        if (player.CurState == PlayerState.GroundSmash ||
            player.CurState == PlayerState.Attack ||
            player.CurState == PlayerState.Dash ||
            player.CurState == PlayerState.Dead) { return; }

        if (player.CurState == PlayerState.Jump) { canUse = false; }

        if (context.duration < .5f && context.canceled)
        {
            print("Shot");
            anim.SetTrigger("Shot"); // test��
        }
        else if (context.duration > 1f && (context.performed || context.canceled) && canUse)
        {
            FocusHeal();
        }
    }

    private void FocusHeal()
    {
        if (!canChargerHeal)
        {
            return; //TODO:�׽�Ʈ�� ���� true�� �س����� ���� �߰��ϰ� ���� �ʿ�
        }

        if (chargeHealCoroutine == null)
        {
            chargeHealCoroutine = StartCoroutine(ChargeHeal());
        }
        else
        {
            print("StopCharge");
            anim.SetBool("isChargeHeal", false);
            player.SetCurState(PlayerState.Idle);
            StopCoroutine(chargeHealCoroutine);
            chargeHealCoroutine = null;
        }
    }

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


    IEnumerator ChargeHeal()
    {
        float curTime = 0f;
        player.SetCurState(PlayerState.ChargeHeal);

        anim.SetBool("isChargeHeal", true);

        print("StartHeal");

        while (true)
        {
            curTime += Time.deltaTime;

            if (curTime > 1f)
            {
                if (/*player.CurHp == player.MaxHp || */player.CurMp < 20)
                {
                    break;
                }
                else
                {
                    curTime = .2f;
                }

                print("Heal");
                player.UseMp(30);
                player.Heal(20);
            }
            yield return null;
        }
    }
}

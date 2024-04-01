using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    PlayerMove playerMove;
    Player player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerMove>();
        player = GetComponent<Player>();
    }

    public void GroundSmash(InputAction.CallbackContext context)
    {
        // TODO: �� ��ų�� �ر��Ͽ��ٸ� ��밡��
        if (!playerMove.isGround && canGrandSmash && context.started)
        {
            print("Smash");
            StartCoroutine(Smash());
        }
    }

    public void SpellOrHeal(InputAction.CallbackContext context)
    {
        bool canUse = true;

        if (player.CurState == PlayerState.GROUNDSMASH ||
            player.CurState == PlayerState.ATTACK ||
            player.CurState == PlayerState.DASH ||
            player.CurState == PlayerState.DEAD) { return; }

        if (player.CurState == PlayerState.JUMP) { canUse = false; }

        if (context.duration < .5f && context.canceled)
        {
            print("Shot");
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
            player.SetCurState(PlayerState.IDLE);
            StopCoroutine(chargeHealCoroutine);
            chargeHealCoroutine = null;
        }
    }

    IEnumerator Smash()
    {
        float curTime = 0f;
        canGrandSmash = false;
        player.SetCurState(PlayerState.GROUNDSMASH);
        float originGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        playerMove.StopMove();
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
        player.SetCurState(PlayerState.IDLE);
        yield return new WaitForSeconds(smashCD);
        canGrandSmash = true;
        blockLoop = false;
    }


    IEnumerator ChargeHeal()
    {
        float curTime = 0f;
        bool heal = false;
        player.SetCurState(PlayerState.CHARGEHEAL);

        print("StartHeal");

        while (!heal)
        {
            curTime += Time.deltaTime;

            if (curTime > 1f)
            {
                print("Heal");

                if (player.CurHp == player.MaxHp /*test code */ && heal/*|| ���� �� ��*/)
                {
                    heal = true;
                }
                else
                {
                    curTime = .2f;
                }
            }
            yield return null;
        }
    }
}

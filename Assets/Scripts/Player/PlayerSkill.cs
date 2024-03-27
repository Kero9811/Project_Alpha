using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkill : MonoBehaviour
{
    #region �׶��� ���Ž�
    private float fallSpeed = 15f;
    private bool canGrandSmash = true;
    private bool isSmashing;
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

    void OnGroundSmash(InputValue value)
    {
        // TODO: �� ��ų�� �ر��Ͽ��ٸ� ��밡��
        if (canGrandSmash)
        {
            print("Smash");
            StartCoroutine(Smash());
        }
    }

    void OnChargeHeal(InputValue value)
    {
        if(!canChargerHeal)
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
        isSmashing = true;
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
        isSmashing = false;
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

            if (curTime > 2f)
            {
                print("Heal");

                if(player.CurHp == player.MaxHp /*|| ���� �� ��*/)
                {
                    heal = true;
                }
                else
                {
                    curTime = 1.5f;
                }
            }
            yield return null;
        }
    }
}

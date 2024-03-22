using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GroundSmash : MonoBehaviour
{
    private float fallSpeed = 15f;
    private bool canGrandSmash = true; // ���¸ӽ� ���� �� �ӽ� ���
    private bool isSmashing;
    private float smashCD = 1f;

    private bool blockLoop = false; // ���Ž� ���� ���� ����

    Rigidbody2D rb;
    PlayerMove playerMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerMove>();
    }

    void OnGroundSmash(InputValue value)
    {
        // ��� ���缭 ���� ��¦ ���ٰ� �Ʒ��� ��
        //TODO: ���¸ӽ� ������ ���� �����ϱ� (���Ž� ���� �� �� �����̵���)
        if (canGrandSmash)
        {
            print("Smash");
            StartCoroutine(Smash());
        }
    }

    IEnumerator Smash()
    {
        float curTime = 0f;
        canGrandSmash = false;
        isSmashing = true;
        float originGravity = rb.gravityScale;
        rb.gravityScale = 0f;
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
        yield return new WaitForSeconds(smashCD);
        canGrandSmash = true;
        blockLoop = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GroundSmash : MonoBehaviour
{
    private float fallSpeed = 15f;
    private bool canGrandSmash = true; // 상태머신 적용 전 임시 사용
    private bool isSmashing;
    private float smashCD = 1f;

    private bool blockLoop = false; // 스매싱 무한 루프 방지

    Rigidbody2D rb;
    PlayerMove playerMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerMove>();
    }

    void OnGroundSmash(InputValue value)
    {
        // 잠깐 멈춰서 위로 살짝 떴다가 아래로 쭉
        //TODO: 상태머신 빠르게 만들어서 통합하기 (스매쉬 중일 때 못 움직이도록)
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

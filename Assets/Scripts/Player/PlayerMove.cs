using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private Vector2 inputVec;
    private Vector2 lastMoveDir; // 마지막으로 이동한 방향
    private Transform footTf;
    private bool isGround;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float dashForce;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody2D rb;

    private bool isDash; // 상태머신 적용 전 임시 사용

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundMask = LayerMask.GetMask("Ground");
        footTf = transform.GetChild(0).GetComponent<Transform>();
        lastMoveDir = transform.right;
    }

    private void FixedUpdate()
    {
        CheckGround();

        Vector2 moveVelocity = new Vector2(inputVec.x * moveSpeed, rb.velocity.y);
        rb.velocity = moveVelocity;

        // 플레이어가 이동한 방향을 기록
        if (inputVec != Vector2.zero)
        {
            lastMoveDir = inputVec.normalized;
        }

        DebugLine();
    }

    void OnMove(InputValue value)
    {
        float x = Mathf.Abs(transform.localScale.x);
        float y = Mathf.Abs(transform.localScale.y);

        inputVec = value.Get<Vector2>();

        // InputVec의 값에 따른 플레이어 방향 전환
        if (inputVec.x < 0)
        {
            transform.localScale = new Vector2(-x, y);
        }
        else if (inputVec.x > 0)
        {
            transform.localScale = new Vector2(x, y);
        }
        else {  }
    }

    void OnJump(InputValue value)
    {
        //TODO: DoubleJump
        if (isGround)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    void OnDash(InputValue value)
    {
        //TODO: Dash
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        float dashTime = 0f;
        isDash = true;

        while (isDash)
        {
            dashTime += Time.deltaTime;

            //if (lastMoveDir == Vector2.zero) lastMoveDir = Vector2.right;
            rb.velocity = 
                new Vector2(lastMoveDir.x * (moveSpeed * 5f), rb.velocity.y);
            print(rb.velocity);

            if (dashTime >= .5f)
            {
                dashTime = 0;
                isDash = false;
            }
            yield return null;
        }
    }

    private void CheckGround()
    {
        if (Physics2D.Raycast(footTf.position, Vector2.down, .1f, groundMask))
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }

    private void DebugLine()
    {
        Debug.DrawRay(footTf.position, Vector2.down * .1f, Color.red);
    }
}

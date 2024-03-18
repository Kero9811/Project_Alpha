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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundMask = LayerMask.GetMask("Ground");
        footTf = transform.GetChild(0).GetComponent<Transform>();
        lastMoveDir = Vector2.right;
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
        rb.AddForce(lastMoveDir * dashForce, ForceMode2D.Impulse);
        Debug.Log(lastMoveDir);
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

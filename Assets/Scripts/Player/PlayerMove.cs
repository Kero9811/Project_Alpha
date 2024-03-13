using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private Vector2 inputVec;
    private Vector2 lastMoveDirection; // 마지막으로 이동한 방향
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
        lastMoveDirection = Vector2.right;
    }

    private void FixedUpdate()
    {
        CheckGround();
        Vector2 moveVelocity = new Vector2(inputVec.x * moveSpeed, rb.velocity.y);
        rb.velocity = moveVelocity;

        // 플레이어가 이동한 방향을 기록
        if (inputVec != Vector2.zero)
        {
            lastMoveDirection = inputVec.normalized;
        }

        DebugLine();
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (isGround)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    void OnDash(InputValue value)
    {
        rb.AddForce(lastMoveDirection * dashForce, ForceMode2D.Impulse);
        Debug.Log(lastMoveDirection);
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

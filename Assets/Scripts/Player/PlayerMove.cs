using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Player Moving Value")]

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float dashPower = 24f;

    private Vector2 inputVec;
    private Vector2 lastMoveDir; // ���������� �̵��� ����
    private Transform footTf;

    public bool isGround;
    private LayerMask groundMask;


    private bool canDash = true; // ���¸ӽ� ���� �� �ӽ� ���
    private bool isDashing;
    private float dashingTime = .2f;
    private float dashCD = 1f;

    private bool canDoubleJump = false;

    private Rigidbody2D rb;

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

        if(isDashing) { return; }

        Vector2 moveVelocity = new Vector2(inputVec.x * moveSpeed, rb.velocity.y);
        rb.velocity = moveVelocity;

        // �÷��̾ �̵��� ������ ���
        if (inputVec != Vector2.zero)
        {
            lastMoveDir = inputVec.normalized;
        }

        DebugLine();
    }

    void OnMove(InputValue value)
    {
        if (isDashing) { return; }

        float x = Mathf.Abs(transform.localScale.x);
        float y = Mathf.Abs(transform.localScale.y);

        inputVec = value.Get<Vector2>();

        // InputVec�� ���� ���� �÷��̾� ���� ��ȯ
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
        if (isGround)
        {
            canDoubleJump = true;
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        else if(!isGround && canDoubleJump)
        {
            canDoubleJump = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        else { }
    }

    void OnDash(InputValue value)
    {
        if (canDash)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCD);
        canDash = true;
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

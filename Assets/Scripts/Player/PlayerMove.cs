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
    private Transform footTf;

    public bool isGround;
    private LayerMask groundMask;


    private bool canDash = true;
    private bool isDashing;
    private float dashingTime = .2f;
    private float dashCD = 1f;

    private bool canDoubleJump = false;

    private Rigidbody2D rb;
    Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        groundMask = LayerMask.GetMask("Ground");
        footTf = transform.GetChild(0).GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        CheckGround();

        if (isDashing) { return; }

        Vector2 moveVelocity = new Vector2(inputVec.x * moveSpeed, rb.velocity.y);
        rb.velocity = moveVelocity;

        DebugLine();
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (player.CurState == PlayerState.DASH ||
            player.CurState == PlayerState.CHARGEHEAL ||
            player.CurState == PlayerState.GROUNDSMASH ||
            player.CurState == PlayerState.DEAD) { return; }

        float x = Mathf.Abs(transform.localScale.x);
        float y = Mathf.Abs(transform.localScale.y);

        //inputVec = value.Get<Vector2>();
        inputVec = context.ReadValue<Vector2>();

        if (rb.velocity.y == 0)
        {
            player.SetCurState(PlayerState.MOVE);
        }

        // InputVec의 값에 따른 플레이어 방향 전환
        if (inputVec.x < 0)
        {
            transform.localScale = new Vector2(-x, y);
        }
        else if (inputVec.x > 0)
        {
            transform.localScale = new Vector2(x, y);
        }
        else { }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (player.CurState == PlayerState.DASH ||
           player.CurState == PlayerState.GROUNDSMASH ||
           player.CurState == PlayerState.CHARGEHEAL ||
           player.CurState == PlayerState.DEAD) { return; }

        if (isGround && context.started)
        {
            canDoubleJump = true;
            player.SetCurState(PlayerState.JUMP);
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        else if (!isGround && canDoubleJump && context.started)
        {
            canDoubleJump = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            player.SetCurState(PlayerState.JUMP);
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        else { }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (canDash && context.started)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        PlayerState state = player.CurState;
        canDash = false;
        isDashing = true;
        player.SetCurState(PlayerState.DASH);
        float originGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        player.SetCurState(state);
        rb.velocity = Vector2.zero;
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

            if (player.CurState == PlayerState.DASH ||
               player.CurState == PlayerState.CHARGEHEAL) { return; }

            if (rb.velocity == Vector2.zero && inputVec == Vector2.zero)
            {
                player.SetCurState(PlayerState.IDLE);
            }
            else if (rb.velocity.y == 0 && inputVec != Vector2.zero)
            {
                player.SetCurState(PlayerState.MOVE);
            }
            else { }
        }
        else
        {
            isGround = false;
        }
    }

    public void StopMove()
    {
        inputVec = Vector2.zero;
    }

    private void DebugLine()
    {
        Debug.DrawRay(footTf.position, Vector2.down * .1f, Color.red);
    }
}

using Cinemachine;
using Lean.Pool;
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
    private Transform frontTf;
    private Transform upTf;
    private Transform downTf;

    [HideInInspector] public bool isGround;
    private bool isWall;
    private LayerMask groundMask;


    private bool canDash = true;
    private float dashingTime = .2f;
    private float dashCD = 1f;

    private bool canDoubleJump = false;

    private bool canWallSlide = false;
    private float wallSlideSpeed = 2f;
    private float wallJumpPower = 5f;

    private Rigidbody2D rb;
    private Player player;
    private Animator anim;
    [SerializeField] private CinemachineVirtualCamera vcam;

    #region 이펙트 프리팹
    [SerializeField] private GameObject dashEffectPrefab;
    [SerializeField] private GameObject jumpEffectPrefab;
    [SerializeField] private GameObject doubleJumpEffectPrefab;
    #endregion

    private void Awake()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        anim = transform.Find("Renderer").GetComponent<Animator>();

        groundMask = LayerMask.GetMask("Ground");
        footTf = transform.Find("Foot").GetComponent<Transform>();
        frontTf = transform.Find("Front").GetComponent<Transform>();
        upTf = transform.Find("Up").GetComponent<Transform>();
        downTf = transform.Find("Down").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        CheckGround();
        CheckWall();

        if (player.CurState == PlayerState.Dash ||
            player.CurState == PlayerState.GroundSmash ||
            player.CurState == PlayerState.WallJump) { return; }

        Vector2 moveVelocity = new Vector2(inputVec.x * moveSpeed, rb.velocity.y);
        rb.velocity = moveVelocity;

        anim.SetInteger("Move", (int)player.CurState);
    }

    private void Move(InputAction.CallbackContext context)
    {
        if (player.CurState == PlayerState.Dash ||
            player.CurState == PlayerState.ChargeHeal ||
            player.CurState == PlayerState.GroundSmash ||
            player.CurState == PlayerState.LookAt ||
            player.CurState == PlayerState.Dead) { return; }

        float x = Mathf.Abs(transform.localScale.x);
        float y = Mathf.Abs(transform.localScale.y);

        if (rb.velocity.y == 0)
        {
            player.SetCurState(PlayerState.Move);
        }

        // InputVec의 값에 따른 플레이어 방향 전환
        if (inputVec.x < 0)
        {
            transform.localScale = new Vector3(-x, y, 1);
        }
        else if (inputVec.x > 0)
        {
            transform.localScale = new Vector3(x, y, 1);
        }
        else { }

        if (isWall && !isGround && canWallSlide)
        {
            anim.SetBool("isWallSlide", true);
            player.SetCurState(PlayerState.WallSlide);
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
            _ = StartCoroutine(WallSlideCoroutine());
        }
        inputVec = context.ReadValue<Vector2>();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (player.CurState == PlayerState.Dash ||
           player.CurState == PlayerState.LookAt ||
           player.CurState == PlayerState.GroundSmash ||
           player.CurState == PlayerState.ChargeHeal ||
           player.CurState == PlayerState.Dead) { return; }

        if (player.CurState == PlayerState.WallSlide && context.started)
        {
            // 벽에 붙어 있을 시 할 행동
            StopCoroutine(WallSlideCoroutine());
            anim.SetTrigger("WallJump");
            rb.velocity = Vector2.zero;
            rb.gravityScale = 1.5f;
            player.SetCurState(PlayerState.WallJump);
            rb.AddForce(-frontTf.transform.up * wallJumpPower + Vector3.up * jumpPower, ForceMode2D.Impulse);
            Invoke("AnimParamChange", .5f);
            return;
        }

        if (isGround && context.started)
        {
            canDoubleJump = true;
            player.SetCurState(PlayerState.Jump);

            // Jump Effect
            LeanPool.Spawn(jumpEffectPrefab, footTf.position, Quaternion.identity);

            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
        }
        else if (!isGround && canDoubleJump && context.started)
        {
            canDoubleJump = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            player.SetCurState(PlayerState.Jump);

            // DoubleJump Effect
            LeanPool.Spawn(doubleJumpEffectPrefab, footTf.position, Quaternion.identity);

            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetTrigger("DoubleJump");
        }
        else { }
    }

    private void LookUp(InputAction.CallbackContext context)
    {
        if (player.CurState != PlayerState.Idle &&
            player.CurState != PlayerState.LookAt) { return; }


        if (context.duration > 1f && context.performed)
        {
            player.SetCurState(PlayerState.LookAt);
            anim.SetBool("isLookUp", true);
            //vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 0f;
            vcam.Follow = upTf;
        }
        else if (context.duration > 1f && context.canceled)
        {
            player.SetCurState(PlayerState.Idle);
            anim.SetBool("isLookUp", false);
            //Vector2 targetPosition = transform.position;
            //vcam.transform.position = targetPosition;
            //vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 0f;
            vcam.Follow = transform;
        }
    }

    private void LookDown(InputAction.CallbackContext context)
    {
        if (player.CurState != PlayerState.Idle &&
            player.CurState != PlayerState.LookAt) { return; }

        Vector2 targetPosition = vcam.transform.position;

        if (context.duration > 1f && context.performed)
        {
            player.SetCurState(PlayerState.LookAt);
            anim.SetBool("isLookDown", true);
            //vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 0f;
            vcam.Follow = downTf;
        }
        else if (context.duration > 1f && context.canceled)
        {
            player.SetCurState(PlayerState.Idle);
            anim.SetBool("isLookDown", false);
            //vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 0f;
            vcam.Follow = transform;
            //vcam.ForceCameraPosition(targetPosition, Quaternion.identity);
        }
    }

    private void Dash(InputAction.CallbackContext context)
    {
        if (player.CurState == PlayerState.GroundSmash ||
            player.CurState == PlayerState.WallSlide ||
            player.CurState == PlayerState.LookAt ||
            player.CurState == PlayerState.Dead) { return; }

        if (canDash && context.started)
        {
            _ = StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        anim.SetBool("isDashing", true);
        PlayerState state = player.CurState;
        canDash = false;
        player.SetCurState(PlayerState.Dash);
        float originGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        // Dash Effect
        LeanPool.Spawn(dashEffectPrefab, footTf, false);

        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        player.SetCurState(state);
        rb.velocity = Vector2.zero;
        rb.gravityScale = originGravity;
        anim.SetBool("isDashing", false);
        yield return new WaitForSeconds(dashCD);
        canDash = true;
    }

    public void DownAttackJump()
    {
        player.SetCurState(PlayerState.Jump);

        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        //anim.SetTrigger("Jump");
    }

    private void CheckGround()
    {
        if (Physics2D.Raycast(footTf.position, Vector2.down, .1f, groundMask))
        {
            isGround = true;
            anim.SetBool("isGround", isGround);

            if (player.CurState == PlayerState.Dash ||
               player.CurState == PlayerState.ChargeHeal ||
               player.CurState == PlayerState.GroundSmash ||
               player.CurState == PlayerState.WallSlide ||
               player.CurState == PlayerState.LookAt ||
               player.CurState == PlayerState.Dead) { return; }

            if (rb.velocity == Vector2.zero && inputVec == Vector2.zero)
            {
                player.SetCurState(PlayerState.Idle);
            }
            else if (rb.velocity.y == 0 && inputVec != Vector2.zero)
            {
                player.SetCurState(PlayerState.Move);
            }
            else { }
        }
        else
        {
            isGround = false;
            anim.SetBool("isGround", isGround);
        }
    }

    private void CheckWall()
    {
        isWall = Physics2D.Raycast(frontTf.position, frontTf.transform.up, .2f, groundMask) ? true : false;
    }

    #region 벽타기 코루틴
    IEnumerator WallSlideCoroutine()
    {
        while (player.CurState == PlayerState.WallSlide)
        {
            rb.velocity = Vector2.down * wallSlideSpeed;
            if (isGround)
            {
                anim.SetBool("isWallSlide", false);
                player.SetCurState(PlayerState.Idle);
                rb.gravityScale = 1.5f;
                break;
            }
            else if (!isGround && !isWall)
            {
                anim.SetBool("isWallSlide", false);
                player.SetCurState(PlayerState.Jump);
                rb.gravityScale = 1.5f;
                break;
            }
            yield return null;
        }
    }
    #endregion

    public void StopMove()
    {
        inputVec = Vector2.zero;
    }

    private void AnimParamChange()
    {
        anim.SetBool("isWallSlide", false);
        player.SetCurState(PlayerState.Jump);
    }

    public void UnlockWallSlide()
    {
        canWallSlide = true;
    }

    private void DebugLine()
    {
        Debug.DrawRay(footTf.position, Vector2.down * .1f, Color.red);
    }
}

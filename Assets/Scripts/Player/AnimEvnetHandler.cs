using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvnetHandler : MonoBehaviour
{
    Animator anim;
    Player player;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GetComponentInParent<Player>();
    }

    private void StopAnim()
    {
        if (player.P_Move.isGround) { return; }
        anim.speed = 0f;
    }

    private void SetStateToIdle()
    {
        player.SetCurState(PlayerState.Idle);
    }
}
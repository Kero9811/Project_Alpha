using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Destroy(animator.gameObject);
        LeanPool.Despawn(animator.gameObject);
    }
}

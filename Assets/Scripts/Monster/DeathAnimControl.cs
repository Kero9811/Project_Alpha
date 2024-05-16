using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimControl : MonoBehaviour
{
    Death death;

    private void Awake()
    {
        death = GetComponentInParent<Death>();
    }

    private void SetAnimSpeedToOrigin(float speed)
    {
        if (death == null) { print("death"); }
        if (death.anim == null) { print("anim"); }
        death.anim.speed = speed;
    }
}
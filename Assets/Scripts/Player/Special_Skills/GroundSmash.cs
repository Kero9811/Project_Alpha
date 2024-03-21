using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GroundSmash : MonoBehaviour
{
    private float fallSpeed = 15f;

    void OnGroundSmash(InputValue value)
    {
        print("Test");
    }

    void OnMultiTap(InputValue value)
    {
        print("Test2");
    }
}

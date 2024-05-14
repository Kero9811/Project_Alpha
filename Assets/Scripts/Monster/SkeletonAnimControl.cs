using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimControl : MonoBehaviour
{
    Skeleton skeleton;

    private void Awake()
    {
        skeleton = GetComponentInParent<Skeleton>();
    }

    private void OnIsAction()
    {
        skeleton.DeactivateisAction();
    }
}
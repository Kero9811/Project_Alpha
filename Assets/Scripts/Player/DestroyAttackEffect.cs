using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAttackEffect : MonoBehaviour
{
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        _ = StartCoroutine(CheckEffect());
    }

    IEnumerator CheckEffect()
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);

            if (!ps.IsAlive(true))
            {
                Lean.Pool.LeanPool.Despawn(gameObject);
                break;
            }
        }
    }
}
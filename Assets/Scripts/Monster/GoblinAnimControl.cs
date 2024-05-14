using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAnimControl : MonoBehaviour
{
    Goblin goblin;

    private void Awake()
    {
        goblin = GetComponentInParent<Goblin>();
    }

    private void OnGoblinControl()
    {
        goblin.SetGoblinState();
    }
}
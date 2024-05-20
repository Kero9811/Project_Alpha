using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstRune : RuneItem
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            GameManager.Instance.Inven.AddRuneToSlotQueue(this.runeInfo);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAbility : Item
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Debug.Log("¥…∑¬ æ∆¿Ã≈€ »πµÊ!");
            GameManager.Instance.Inven.AddItemsToAbilityQueue(this.Info);
        }
    }
}
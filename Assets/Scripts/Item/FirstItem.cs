using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstItem : Item
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            print("√ππ¯¬∞ æ∆¿Ã≈€ »πµÊ!");
            GameManager.Instance.Inven.AddItemsToQueue(this);
        }
    }

    public override void Use()
    {
    }
}
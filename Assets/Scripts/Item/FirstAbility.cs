using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAbility : Item
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            print("�ɷ� ������ ȹ��!");
            GameManager.Instance.Inven.AddItemsToAbilityQueue(this);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAbility : Item
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.P_Move.UnlockDoubleJump();
            GameManager.Instance.Inven.AddItemsToAbilityQueue(this.Info);
            GameManager.Instance.Data.SavePlayerData(player);
        }
    }
}
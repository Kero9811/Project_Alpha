using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondAbility : Item
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.P_Move.UnlockWallSlide();
            GameManager.Instance.Inven.AddItemsToAbilityQueue(this.Info);
            GameManager.Instance.Data.SavePlayerData(player);
        }
    }
}

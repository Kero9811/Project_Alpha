using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondAbility : Item
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            player.P_Move.UnlockWallSlide();
            GameManager.Instance.Inven.AddItemsToAbilityQueue(this.Info);
            GameManager.Instance.Data.SavePlayerData(player);
            Invoke("DestroyItem", 2f);
        }
    }

    private void DestroyItem()
    {
        Destroy(gameObject);
    }
}

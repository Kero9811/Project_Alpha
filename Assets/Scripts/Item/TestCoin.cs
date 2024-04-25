using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoin : Item
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            //player.GetGold(cost);
            Destroy(gameObject);
        }
    }
}
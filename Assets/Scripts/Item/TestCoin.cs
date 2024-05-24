using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoin : Item
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.GetGold(999);
            player.P_Attack.MeleeDmgUp(10);
            player.GetMp(50);
        }
    }
}
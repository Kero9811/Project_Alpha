using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHpItem : Item
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            player.GetMaxHp(1);
            Debug.Log("MaxHp Up!");
        }
    }
}
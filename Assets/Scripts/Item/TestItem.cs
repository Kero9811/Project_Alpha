using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : Item
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            player.P_Move.UnlockWallSlide();
            Debug.Log("WallSlide is Unlocked!");
            Destroy(gameObject);
        }
    }
}
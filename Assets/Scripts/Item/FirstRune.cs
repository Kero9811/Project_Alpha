using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstRune : RuneItem
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            GameManager.Instance.Inven.AddRuneToSlotQueue(this.runeInfo);
            Invoke("DestroyItem", 2f);
        }
    }

    private void DestroyItem()
    {
        Destroy(gameObject);
    }
}
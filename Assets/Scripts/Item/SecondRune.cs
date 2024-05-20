using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondRune : RuneItem
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            GameManager.Instance.Inven.AddRuneToSlotQueue(this.runeInfo);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            Invoke("DestroyItem", 2f);
        }
    }

    private void DestroyItem()
    {
        Destroy(gameObject);
    }
}
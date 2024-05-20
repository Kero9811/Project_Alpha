using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondItem : Item
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            GameManager.Instance.Inven.AddItemsToStoryQueue(this.Info);
            GameManager.Instance.Data.SaveTalkIndex(11, false, true);
            Invoke("DestroyItem", 2f);
        }
    }

    private void DestroyItem()
    {
        Destroy(gameObject);
    }
}
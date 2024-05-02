using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstItem : Item
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Debug.Log("Ω∫≈‰∏Æ æ∆¿Ã≈€ »πµÊ!");
            GameManager.Instance.Inven.AddItemsToStoryQueue(this.data);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstItem : Item
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Debug.Log("���丮 ������ ȹ��!");
            GameManager.Instance.Inven.AddItemsToStoryQueue(this.data);
        }
    }
}
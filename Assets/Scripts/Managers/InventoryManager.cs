using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Queue<ItemData> storyItemQueue = new Queue<ItemData>();
    public Queue<ItemData> abilityItemQueue = new Queue<ItemData>();

    public Queue<RuneItemData> runeItemQueue = new Queue<RuneItemData>();

    public void AddItemsToStoryQueue(ItemData item)
    {
        if (storyItemQueue.Contains(item)) { return; }
        storyItemQueue.Enqueue(item);
    }

    public void AddItemsToAbilityQueue(ItemData item)
    {
        if (abilityItemQueue.Contains(item)) { return; }
        abilityItemQueue.Enqueue(item);
    }

    public void AddRuneToSlotQueue(RuneItemData runeItem)
    {
        if (runeItemQueue.Contains(runeItem)) {  return; }
        if (runeItem.isEquipped) { runeItem.isEquipped = false; }
        runeItemQueue.Enqueue(runeItem);
        GameManager.Instance.Data.SaveRuneItem(runeItemQueue);
    }
}
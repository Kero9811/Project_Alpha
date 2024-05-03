using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Queue<ItemData> storyItemQueue = new Queue<ItemData>();
    public Queue<ItemData> abilityItemQueue = new Queue<ItemData>();
    //public Queue<ObjData> storyItemQueue = new Queue<ObjData>();
    //public Queue<ObjData> abilityItemQueue = new Queue<ObjData>();

    public Queue<RuneItemData> runeItemQueue = new Queue<RuneItemData>();

    public void AddItemsToStoryQueue(ItemData item)
    {
        storyItemQueue.Enqueue(item);
    }

    //public void AddItemsToStoryQueue(ObjData item)
    //{
    //    storyItemQueue.Enqueue(item);
    //}

    public void AddItemsToAbilityQueue(ItemData item)
    {
        abilityItemQueue.Enqueue(item);
    }

    //public void AddItemsToAbilityQueue(ObjData item)
    //{
    //    abilityItemQueue.Enqueue(item);
    //}

    public void AddRuneToSlotQueue(RuneItemData runeItem)
    {
        runeItemQueue.Enqueue(runeItem);
    }
}
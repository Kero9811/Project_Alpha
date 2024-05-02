using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //public Queue<Item> storyItemQueue = new Queue<Item>();
    //public Queue<Item> abilityItemQueue = new Queue<Item>();
    public Queue<ObjData> storyItemQueue = new Queue<ObjData>();
    public Queue<ObjData> abilityItemQueue = new Queue<ObjData>();

    public Queue<RuneItem> runeItemQueue = new Queue<RuneItem>();

    //public void AddItemsToStoryQueue(Item item)
    //{
    //    storyItemQueue.Enqueue(item);
    //}

    public void AddItemsToStoryQueue(ObjData item)
    {
        storyItemQueue.Enqueue(item);
    }

    //public void AddItemsToAbilityQueue(Item item)
    //{
    //    abilityItemQueue.Enqueue(item);
    //}

    public void AddItemsToAbilityQueue(ObjData item)
    {
        abilityItemQueue.Enqueue(item);
    }

    public void AddRuneToSlotQueue(RuneItem runeItem)
    {
        runeItemQueue.Enqueue(runeItem);
    }
}
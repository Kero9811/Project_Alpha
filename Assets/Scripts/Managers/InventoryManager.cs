using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //[SerializeField] private Inventory inventory;

    public Queue<Item> storyItemQueue = new Queue<Item>();
    public Queue<Item> abilityItemQueue = new Queue<Item>();
    public Queue<RuneItem> runeItemQueue = new Queue<RuneItem>();

    public int storyCount = 0; // �׽�Ʈ�� ����
    public int abilityCount = 0; // �׽�Ʈ�� ����
    public int runeCount = 0; // �׽�Ʈ�� ����

    private void Awake()
    {
        //if (!GameManager.Instance.CheckIsGameScene()) { return; }

        //GameObject uiCanvas = GameObject.FindWithTag("Canvas");

        //inventory = uiCanvas.transform.Find("InventoryPanel").GetComponentInChildren<Inventory>();
    }

    public void AddItemsToStoryQueue(Item item)
    {
        storyItemQueue.Enqueue(item);
        storyCount++;
    }

    public void AddItemsToAbilityQueue(Item item)
    {
        abilityItemQueue.Enqueue(item);
        abilityCount++;
    }

    public void AddRuneToSlotQueue(RuneItem runeItem)
    {
        runeItemQueue.Enqueue(runeItem);
        runeCount++;
    }

    //public void AddRuneToEquipQueue(RuneItem runeItem)
    //{

    //}
}
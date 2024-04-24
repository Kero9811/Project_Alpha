using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //[SerializeField] private Inventory inventory;

    public Queue<Item> itemQueue = new Queue<Item>();

    public int queueCount = 0;

    private void Awake()
    {
        //if (!GameManager.Instance.CheckIsGameScene()) { return; }

        //GameObject uiCanvas = GameObject.FindWithTag("Canvas");

        //inventory = uiCanvas.transform.Find("InventoryPanel").GetComponentInChildren<Inventory>();
    }

    public void AddItemsToQueue(Item item)
    {
        itemQueue.Enqueue(item);
        queueCount++;
    }

    
}
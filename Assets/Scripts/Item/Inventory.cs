using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    private Slot[] slots;

    [SerializeField] private Transform slotParent;
    [SerializeField] private Transform descParent;

    private void Awake()
    {
        slots = new Slot[slotParent.childCount];

        for (int i = 0; i < slotParent.childCount; i++)
        {
            slots[i] = slotParent.GetChild(i).GetComponentInChildren<Slot>();
        }

        UpdateSlot();
    }

    private void OnEnable()
    {
        if (GameManager.Instance?.Inven != null)
        {
            AddItemsFromQueue(GameManager.Instance.Inven.itemQueue);
        }
    }

    public void UpdateSlot()
    {
        int i = 0;
        for (; i < items.Count && i < slots.Length; i++)
        {
            items[i].InitItemInfo();
            slots[i].item = items[i];
            slots[i].SetItem(items[i]);
        }
        for (; i < slots.Length; i++)
        {
            slots[i].item = null;
            slots[i].SetItem(null);
        }
    }

    public void AddItem(Item item)
    {
        if (items.Count < slots.Length)
        {
            items.Add(item);
            UpdateSlot();
        }
        else
        {
            // 게임 구조상 발생할 일 없음
            Debug.Log("Inventory is Full");
        }
    }

    public void ConfirmItemInfo(Item item)
    {
        if (item != null)
        {
            Item targetItem = items.Find(x => x.id ==  item.id);
            Image targetImage = descParent.Find("ItemImage").GetComponent<Image>();
            targetImage.sprite = targetItem.itemSprite;
            targetImage.color = new Color(1, 1, 1, 1);
            descParent.Find("ItemDescText").GetComponent<TextMeshProUGUI>().text = targetItem.desc;
        }
        else
        {
            Image targetImage = descParent.Find("ItemImage").GetComponent<Image>();
            targetImage.sprite = null;
            targetImage.color = new Color(1, 1, 1, 0);
            descParent.Find("ItemDescText").GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public void AddItemsFromQueue(Queue<Item> itemQueue)
    {
        if (itemQueue.Count > 0)
        {
            for (int i = 0; i < itemQueue.Count; i++)
            {
                AddItem(itemQueue.Dequeue());
            }

            GameManager.Instance.Inven.itemQueue.Clear();
        }
    }
}
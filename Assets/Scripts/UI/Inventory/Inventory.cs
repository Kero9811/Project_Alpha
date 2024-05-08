using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemData> storyItems = new List<ItemData>(); // Dictionary가 좋을 수도 있음 (탐색을 위해)
    [SerializeField] private List<ItemData> abilityItems = new List<ItemData>();

    private Slot[] storySlots;
    private Slot[] abilitySlots;

    public ItemData[] itemDatas;

    [SerializeField] private Transform storySlotParent;
    [SerializeField] private Transform abilitySlotParent;
    [SerializeField] private Transform descParent;

    private void Awake()
    {
        storySlots = new Slot[storySlotParent.childCount];
        for (int i = 0; i < storySlotParent.childCount; i++)
        {
            storySlots[i] = storySlotParent.GetChild(i).GetComponentInChildren<Slot>();
        }

        abilitySlots = new Slot[abilitySlotParent.childCount];
        for (int i = 0; i < abilitySlotParent.childCount; i++)
        {
            abilitySlots[i] = abilitySlotParent.GetChild(i).GetComponentInChildren<Slot>();
        }

        UpdateInvenSlot();
    }

    private void Start()
    {
        GameManager.Instance.Data.LoadInvenItem(storyItems, abilityItems, itemDatas);
        UpdateInvenSlot();
    }

    private void OnEnable()
    {
        if (GameManager.Instance?.Inven != null)
        {
            AddStoryItemsFromQueue(GameManager.Instance.Inven.storyItemQueue);
            AddAbilityItemsFromQueue(GameManager.Instance.Inven.abilityItemQueue);
        }
    }

    private void OnDisable()
    {
        ResetItemConfirmUI();
    }

    public void UpdateInvenSlot()
    {
        // 스토리 아이템 업데이트
        int i = 0;
        for (; i < storyItems.Count && i < storySlots.Length; i++)
        {
            storySlots[i].itemData = storyItems[i];
            storySlots[i].SetItem(storyItems[i]);
        }
        for (; i < storySlots.Length; i++)
        {
            storySlots[i].item = null;
            storySlots[i].SetItem(null);
        }

        // 능력 아이템 업데이트
        int j = 0;
        for (; j < abilityItems.Count && j < abilitySlots.Length; j++)
        {
            abilitySlots[j].itemData = abilityItems[j];
            abilitySlots[j].SetItem(abilityItems[j]);
        }
        for (; j < abilitySlots.Length; j++)
        {
            abilitySlots[j].item = null;
            abilitySlots[j].SetItem(null);
        }
    }

    public void AddItemToInven(ItemData item)
    {
        if (storyItems.Count < storySlots.Length && abilityItems.Count < abilitySlots.Length)
        {
            if (item.Id <= 100 && item.Id >= 0)
            {
                storyItems.Add(item);
            }
            else if (item.Id > 100 && item.Id <= 200)
            {
                abilityItems.Add(item);
            }
        }
        else
        {
            // 만들 아이템 갯수가 모자라서 구조상 발생할 일 없음
            Debug.Log("Inventory is Full");
        }
    }

    public void ConfirmItemInfo(ItemData item)
    {
        if (item != null)
        {
            if (item.Id >= 0 && item.Id <= 100)
            {
                ItemData targetItem = storyItems.Find(x => x.Id == item.Id);
                Image targetImage = descParent.Find("ItemImage").GetComponent<Image>();
                targetImage.sprite = targetItem.Image;
                targetImage.color = new Color(1, 1, 1, 1);
                descParent.Find("ItemNameText").GetComponent<TextMeshProUGUI>().text = targetItem.ItemName;
                descParent.Find("ItemDescText").GetComponent<TextMeshProUGUI>().text = targetItem.Desc;
            }
            else if (item.Id > 100 && item.Id <= 200)
            {
                ItemData targetItem = abilityItems.Find(x => x.Id == item.Id);
                Image targetImage = descParent.Find("ItemImage").GetComponent<Image>();
                targetImage.sprite = targetItem.Image;
                targetImage.color = new Color(1, 1, 1, 1);
                descParent.Find("ItemNameText").GetComponent<TextMeshProUGUI>().text = targetItem.ItemName;
                descParent.Find("ItemDescText").GetComponent<TextMeshProUGUI>().text = targetItem.Desc;
            }
        }
        else
        {
            ResetItemConfirmUI();
        }
    }

    private void ResetItemConfirmUI()
    {
        Image targetImage = descParent.Find("ItemImage").GetComponent<Image>();
        targetImage.sprite = null;
        targetImage.color = new Color(1, 1, 1, 0);
        descParent.Find("ItemNameText").GetComponent<TextMeshProUGUI>().text = "";
        descParent.Find("ItemDescText").GetComponent<TextMeshProUGUI>().text = "";
    }

    public void AddStoryItemsFromQueue(Queue<ItemData> itemQueue)
    {
        if (itemQueue.Count > 0)
        {
            while (itemQueue.Count > 0)
            {
                AddItemToInven(itemQueue.Dequeue());
            }

            UpdateInvenSlot();

            GameManager.Instance.Inven.storyItemQueue.Clear();

            GameManager.Instance.Data.SaveInvenItem(storyItems, null);
        }
    }

    public void AddAbilityItemsFromQueue(Queue<ItemData> itemQueue)
    {
        if (itemQueue.Count > 0)
        {
            while (itemQueue.Count > 0)
            {
                AddItemToInven(itemQueue.Dequeue());
            }

            UpdateInvenSlot();

            GameManager.Instance.Inven.abilityItemQueue.Clear();

            GameManager.Instance.Data.SaveInvenItem(null, abilityItems);
        }
    }
}
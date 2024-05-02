using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //[SerializeField] private List<Item> storyItems = new List<Item>(); // Dictionary�� ���� ���� ���� (Ž���� ����)
    //[SerializeField] private List<Item> abilityItems = new List<Item>();
    [SerializeField] private List<ObjData> storyItems = new List<ObjData>(); // Dictionary�� ���� ���� ���� (Ž���� ����)
    [SerializeField] private List<ObjData> abilityItems = new List<ObjData>();

    private Slot[] storySlots;
    private Slot[] abilitySlots;

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
        //GameManager.Instance.Data.LoadInvenItem(storyItems, abilityItems);
        GameManager.Instance.Data.LoadInvenItemData(storyItems, abilityItems);
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
        // ���丮 ������ ������Ʈ
        int i = 0;
        for (; i < storyItems.Count && i < storySlots.Length; i++)
        {
            //storyItems[i].InitItemInfo();
            //storySlots[i].item = storyItems[i];
            //storySlots[i].SetItem(storyItems[i]);

            //storyItems[i].InitItemInfo();
            storySlots[i].itemData = storyItems[i];
            storySlots[i].SetItemData(storyItems[i]);
        }
        for (; i < storySlots.Length; i++)
        {
            storySlots[i].item = null;
            storySlots[i].SetItem(null);

            //storySlots[i].itemData = null;
            //storySlots[i].SetItemData(null);
        }

        // �ɷ� ������ ������Ʈ
        int j = 0;
        for (; j < abilityItems.Count && j < abilitySlots.Length; j++)
        {
            //abilityItems[j].InitItemInfo();
            //abilitySlots[j].item = abilityItems[j];
            //abilitySlots[j].SetItem(abilityItems[j]);

            //abilityItems[j].InitItemInfo();
            abilitySlots[j].itemData = abilityItems[j];
            abilitySlots[j].SetItemData(abilityItems[j]);
        }
        for (; j < abilitySlots.Length; j++)
        {
            //abilitySlots[j].item = null;
            //abilitySlots[j].SetItem(null);

            abilitySlots[j].itemData = null;
            abilitySlots[j].SetItemData(null);
        }
    }

    //public void AddItemToInven(Item item)
    //{
    //    if (storyItems.Count < storySlots.Length && abilityItems.Count < abilitySlots.Length)
    //    {
    //        if (item.id <= 100 && item.id >= 0)
    //        {
    //            storyItems.Add(item);
    //        }
    //        else if (item.id > 100 && item.id <= 200)
    //        {
    //            abilityItems.Add(item);
    //        }
    //    }
    //    else
    //    {
    //        // ���� ������ ������ ���ڶ� ������ �߻��� �� ����
    //        Debug.Log("Inventory is Full");
    //    }
    //}

    public void AddItemToInven(ObjData item)
    {
        if (storyItems.Count < storySlots.Length && abilityItems.Count < abilitySlots.Length)
        {
            if (item.id <= 100 && item.id >= 0)
            {
                storyItems.Add(item);
            }
            else if (item.id > 100 && item.id <= 200)
            {
                abilityItems.Add(item);
            }
        }
        else
        {
            // ���� ������ ������ ���ڶ� ������ �߻��� �� ����
            Debug.Log("Inventory is Full");
        }
    }

    //public void ConfirmItemInfo(Item item)
    //{
    //    if (item != null)
    //    {
    //        if (item.id >= 0 && item.id <= 100)
    //        {
    //            Item targetItem = storyItems.Find(x => x.id == item.id);
    //            Image targetImage = descParent.Find("ItemImage").GetComponent<Image>();
    //            targetImage.sprite = targetItem.itemSprite;
    //            targetImage.color = new Color(1, 1, 1, 1);
    //            descParent.Find("ItemNameText").GetComponent<TextMeshProUGUI>().text = targetItem.itemName;
    //            descParent.Find("ItemDescText").GetComponent<TextMeshProUGUI>().text = targetItem.desc;
    //        }
    //        else if (item.id > 100 && item.id <= 200)
    //        {
    //            Item targetItem = abilityItems.Find(x => x.id == item.id);
    //            Image targetImage = descParent.Find("ItemImage").GetComponent<Image>();
    //            targetImage.sprite = targetItem.itemSprite;
    //            targetImage.color = new Color(1, 1, 1, 1);
    //            descParent.Find("ItemNameText").GetComponent<TextMeshProUGUI>().text = targetItem.itemName;
    //            descParent.Find("ItemDescText").GetComponent<TextMeshProUGUI>().text = targetItem.desc;
    //        }
    //    }
    //    else
    //    {
    //        ResetItemConfirmUI();
    //    }
    //}

    public void ConfirmItemInfo(ObjData item)
    {
        if (item != null)
        {
            if (item.id >= 0 && item.id <= 100)
            {
                ObjData targetItem = storyItems.Find(x => x.id == item.id);
                Image targetImage = descParent.Find("ItemImage").GetComponent<Image>();
                targetImage.sprite = SpriteDeserializer.LoadSpriteFromImage(targetItem.imagePath);
                targetImage.color = new Color(1, 1, 1, 1);
                descParent.Find("ItemNameText").GetComponent<TextMeshProUGUI>().text = targetItem.itemName;
                descParent.Find("ItemDescText").GetComponent<TextMeshProUGUI>().text = targetItem.desc;
            }
            else if (item.id > 100 && item.id <= 200)
            {
                ObjData targetItem = abilityItems.Find(x => x.id == item.id);
                Image targetImage = descParent.Find("ItemImage").GetComponent<Image>();
                targetImage.sprite = SpriteDeserializer.LoadSpriteFromImage(targetItem.imagePath);
                targetImage.color = new Color(1, 1, 1, 1);
                descParent.Find("ItemNameText").GetComponent<TextMeshProUGUI>().text = targetItem.itemName;
                descParent.Find("ItemDescText").GetComponent<TextMeshProUGUI>().text = targetItem.desc;
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

    //public void AddStoryItemsFromQueue(Queue<Item> itemQueue)
    //{
    //    if (itemQueue.Count > 0)
    //    {
    //        while (itemQueue.Count > 0)
    //        {
    //            AddItemToInven(itemQueue.Dequeue());
    //        }

    //        UpdateInvenSlot();

    //        GameManager.Instance.Inven.storyItemQueue.Clear();

    //        GameManager.Instance.Data.SaveInvenItem(storyItems, null);
    //    }
    //}

    public void AddStoryItemsFromQueue(Queue<ObjData> itemQueue)
    {
        if (itemQueue.Count > 0)
        {
            while (itemQueue.Count > 0)
            {
                AddItemToInven(itemQueue.Dequeue());
            }

            UpdateInvenSlot();

            GameManager.Instance.Inven.storyItemQueue.Clear();

            GameManager.Instance.Data.SaveInvenItemData(storyItems, null);
        }
    }

    //public void AddAbilityItemsFromQueue(Queue<Item> itemQueue)
    //{
    //    if (itemQueue.Count > 0)
    //    {
    //        while (itemQueue.Count > 0)
    //        {
    //            AddItemToInven(itemQueue.Dequeue());
    //        }

    //        UpdateInvenSlot();

    //        GameManager.Instance.Inven.abilityItemQueue.Clear();

    //        GameManager.Instance.Data.SaveInvenItem(null, abilityItems);
    //    }
    //}

    public void AddAbilityItemsFromQueue(Queue<ObjData> itemQueue)
    {
        if (itemQueue.Count > 0)
        {
            while (itemQueue.Count > 0)
            {
                AddItemToInven(itemQueue.Dequeue());
            }

            UpdateInvenSlot();

            GameManager.Instance.Inven.abilityItemQueue.Clear();

            GameManager.Instance.Data.SaveInvenItemData(null, abilityItems);
        }
    }
}